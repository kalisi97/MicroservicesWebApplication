using Messages;
using System;
using System.Threading.Tasks;

namespace MessagingBus
{
    public interface IMessageBus
    {
        Task PublishMessage(IntegrationBaseMessage message, string topicName);
    }
}
