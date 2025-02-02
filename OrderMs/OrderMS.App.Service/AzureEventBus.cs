using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OrderMS.App.Infrastructure.Interface;


namespace OrderMS.App.Service
{
    public class AzureEventBus : IEventBus
    {
        private readonly ServiceBusClient _client;

        public AzureEventBus(ServiceBusClient client)
        {
            _client = client;
        }

        public async Task PublishAsync<T>(T @event, string topicName)
        {
            var sender = _client.CreateSender(topicName);
            var message = new ServiceBusMessage(JsonSerializer.Serialize(@event));
            await sender.SendMessageAsync(message);
        }
    }
}
