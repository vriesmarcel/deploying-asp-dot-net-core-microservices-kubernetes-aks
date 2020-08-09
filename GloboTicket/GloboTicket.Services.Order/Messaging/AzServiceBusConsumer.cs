using System;
using System.Threading;
using System.Threading.Tasks;
using GloboTicket.Services.Ordering.Repositories;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GloboTicket.Services.Ordering.Messaging
{
    public class AzServiceBusConsumer
    {
        private readonly string subscriptionName = "globoticketorder";
        private readonly IReceiverClient checkoutMessageReceiverClient;
        private readonly IReceiverClient orderPaymentUpdateMessageReceiverClient;

        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        private readonly OrderRepository _orderRepository;

        public AzServiceBusConsumer(IConfiguration configuration, ILogger logger, OrderRepository orderRepository)
        {
            _configuration = configuration;
            _logger = logger;
            _orderRepository = orderRepository;

            var serviceBusConnectionString = configuration.GetValue<string>("ServiceBusConnectionString");
            var checkoutMessageTopic = configuration.GetValue<string>("CheckoutMessageTopic");
            var orderPaymentRequestMessageTopic = configuration.GetValue<string>("OrderPaymentRequestMessageTopic");
            var orderPaymentUpdatedMessageTopic = configuration.GetValue<string>("OrderPaymentUpdatedMessageTopic");

            checkoutMessageReceiverClient = new SubscriptionClient(serviceBusConnectionString, checkoutMessageTopic, subscriptionName);
            orderPaymentUpdateMessageReceiverClient = new SubscriptionClient(serviceBusConnectionString, orderPaymentUpdatedMessageTopic, subscriptionName);
        }

        public void Start()
        {
            var messageHandlerOptions = new MessageHandlerOptions(OnServiceBusException) {MaxConcurrentCalls = 4};

            checkoutMessageReceiverClient.RegisterMessageHandler(OnCheckoutMessageReceived, messageHandlerOptions);
            orderPaymentUpdateMessageReceiverClient.RegisterMessageHandler(OnOrderPaymentUpdateReceived, messageHandlerOptions);
        }

        private Task OnOrderPaymentUpdateReceived(Message message, CancellationToken arg2)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }

        private Task OnCheckoutMessageReceived(Message message, CancellationToken arg2)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }

        private Task OnServiceBusException(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine(exceptionReceivedEventArgs);

            return Task.CompletedTask;
        }

        public void Stop()
        {
            //TODO
        }
    }
}
