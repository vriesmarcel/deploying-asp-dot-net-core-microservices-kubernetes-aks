using GloboTicket.Integration.MessagingBus;
using GloboTicket.Services.Payment.Messages;
using GloboTicket.Services.Payment.Model;
using GloboTicket.Services.Payment.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GloboTicket.Services.Payment.Worker
{
    public class ServiceBusListener : IHostedService
    {
        private readonly ILogger logger;
        private readonly IConfiguration configuration;
        private ISubscriptionClient _subscriptionClient;
        private readonly IExternalGatewayPaymentService _externalGatewayPaymentService;
        private readonly IMessageBus _messageBus;
        private readonly string orderPaymentUpdatedMessageTopic = string.Empty;

        public ServiceBusListener(IConfiguration configuration, ILoggerFactory loggerFactory, IExternalGatewayPaymentService externalGatewayPaymentService, IMessageBus messageBus)
        {
            this.logger = loggerFactory.CreateLogger<ServiceBusListener>();
            this.configuration = configuration;
            orderPaymentUpdatedMessageTopic = configuration.GetValue<string>("OrderPaymentUpdatedMessageTopic");
            _externalGatewayPaymentService = externalGatewayPaymentService;
            _messageBus = messageBus;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _subscriptionClient = new SubscriptionClient(configuration.GetValue<string>("ServiceBusConnectionString"), configuration.GetValue<string>("OrderPaymentRequestMessageTopic"), configuration.GetValue<string>("subscriptionName"));

            var messageHandlerOptions = new MessageHandlerOptions(e =>
            {
                ProcessError(e.Exception);
                return Task.CompletedTask;
            })
            {
                MaxConcurrentCalls = 3,
                AutoComplete = false
            };

            _subscriptionClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug($"ServiceBusListener stopping.");
            await this._subscriptionClient.CloseAsync();
        }

        protected void ProcessError(Exception e)
        {
            logger.LogError(e, "Error while processing queue item in ServiceBusListener.");
        }

        protected async Task ProcessMessageAsync(Message message, CancellationToken token)
        {
            var messageBody = Encoding.UTF8.GetString(message.Body);
            OrderPaymentRequestMessage orderPaymentRequestMessage = JsonConvert.DeserializeObject<OrderPaymentRequestMessage>(messageBody);

            PaymentInfo paymentInfo = new PaymentInfo
            {
                CardNumber = orderPaymentRequestMessage.CardNumber,
                CardName = orderPaymentRequestMessage.CardName,
                CardExpiration = orderPaymentRequestMessage.CardExpiration,
                Total = orderPaymentRequestMessage.Total
            };

            var result = await _externalGatewayPaymentService.PerformPayment(paymentInfo);

            await this._subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);

            //send payment result to order service via service bus
            OrderPaymentUpdateMessage orderPaymentUpdateMessage = new OrderPaymentUpdateMessage();
            orderPaymentUpdateMessage.PaymentSuccess = result;
            orderPaymentUpdateMessage.OrderId = orderPaymentRequestMessage.OrderId;

            try
            {
                await _messageBus.PublishMessage(orderPaymentUpdateMessage, orderPaymentUpdatedMessageTopic);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            logger.LogDebug($"{orderPaymentRequestMessage.OrderId}: ServiceBusListener received item.");

            await Task.Delay(20000);
            logger.LogDebug($"{orderPaymentRequestMessage.OrderId}:  ServiceBusListener processed item.");
        }
    }
}
