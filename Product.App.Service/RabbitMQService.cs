using EasyNetQ;
using Product.App.Infrastructure.Interface;
using Shared.RabbitMQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.App.Service
{
    public class RabbitMQService :IRabbitMQService<ProductUpdatedMessage>
    {
        private readonly IBus _bus;
        public RabbitMQService(IBus bus)
        {
            _bus = bus;
        }
        // Publish a message to the queue
        public void PublishMessage(ProductUpdatedMessage productMessage)
        {
            _bus.PubSub.Publish(productMessage);
            Console.WriteLine($" [x] Sent: {productMessage.Name}");
        }

        // Subscribe to the queue and consume messages
        public void SubscribeToMessages()
        {
            _bus.PubSub.Subscribe<ProductUpdatedMessage>("product_subscription", message =>
            {
                Console.WriteLine($" [x] Received: {message.Name}, Price: {message.Price}");
            });
        }
    }
}
