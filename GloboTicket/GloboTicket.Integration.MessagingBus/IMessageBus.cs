using GloboTicket.Integration.Messages;
using System.Threading.Tasks;

namespace GloboTicket.Integration.MessagingBus
{
    public interface IMessageBus
    {
        Task PublishMessage (IntegrationBaseMessage message, string topicName);
    }
}
