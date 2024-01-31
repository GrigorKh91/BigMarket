using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace BigMarket.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private string connectionString = @"Endpoint=sb://bigmarket.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=+QfXO9VRB5172U+W1OV1hICOeGwyCLQsS+ASbItsmR4=";
        public async Task PublishMessageAsync(object message, string topic_queue_Name)
        {
            await using var client = new ServiceBusClient(connectionString);
            ServiceBusSender sender = client.CreateSender(topic_queue_Name);
            var jsonMessage = JsonConvert.SerializeObject(message);

            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.
                UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };

            await sender.SendMessageAsync(finalMessage);
            await client.DisposeAsync();
        }
    }
}
