using GloboTicket.Services.PaymentWorker.Model;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GloboTicket.Services.PaymentWorker.Services
{
    public class ServiceBusListener: IHostedService
    {
        private readonly ILogger logger;
        private readonly IConfiguration configuration;
        private QueueClient queueClient;

        public ServiceBusListener(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<ServiceBusListener>();
            this.configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.queueClient = new QueueClient(configuration.GetValue<string>("ServiceBusConnectionString"), configuration.GetValue<string>("ServiceBusQueueName"));

            var messageHandlerOptions = new MessageHandlerOptions(e => {
                ProcessError(e.Exception);
                return Task.CompletedTask;
            })
            {
                MaxConcurrentCalls = 3,
                AutoComplete = false
            };
            this.queueClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug($"ServiceBusListener stopping.");
            await this.queueClient.CloseAsync();
        }

        protected void ProcessError(Exception e)
        {
            logger.LogError(e, "Error while processing queue item in ServiceBusListener.");
        }

        protected async Task ProcessMessageAsync(Message message, CancellationToken token)
        {
            var messageBody = Encoding.UTF8.GetString(message.Body);
            OrderPaymentRequestMessage item = JsonConvert.DeserializeObject<OrderPaymentRequestMessage>(messageBody);

            await this.queueClient.CompleteAsync(message.SystemProperties.LockToken);

            logger.LogDebug($"{item.OrderId} | ServiceBusListener received item.");

            // Take a while - renewing lock fails despite message being completed
            // Exception does not stop execution of this message handler
            await Task.Delay(25000);
            logger.LogDebug($"{item.OrderId} | ServiceBusListener processed item.");
        }
    }
}
