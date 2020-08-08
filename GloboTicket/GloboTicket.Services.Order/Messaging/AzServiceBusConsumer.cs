using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;

namespace GloboTicket.Services.Order.Messaging
{
    public class AzServiceBusConsumer
    {
        private readonly string connectionString = "Endpoint=sb://globoticket.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Hi0hqUzgNIhGOcceT/gW4B23fHSlbVM+FPAxjq3zZTc=";
        private readonly string checkoutMessageTopicName = "checkoutmessage";
        private readonly string orderPaymentUpdateMessageTopicName = "orderpaymentupdatedmessage";
        private readonly string subscriptionName = "globoticketorder";
        private IReceiverClient checkoutMessageReceiverClient;
        private IReceiverClient orderPaymentUpdateMessageReceiverClient;

        public AzServiceBusConsumer()
        {
            checkoutMessageReceiverClient = new SubscriptionClient(connectionString, checkoutMessageTopicName, subscriptionName);
            orderPaymentUpdateMessageReceiverClient = new SubscriptionClient(connectionString, orderPaymentUpdateMessageTopicName, subscriptionName);

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

        private Task OnServiceBusException(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine(arg);

            return Task.CompletedTask;
        }

        public void Stop()
        {
            //TODO
        }
    }
}
