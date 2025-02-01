using AutoMapper;
using AutoMapper.Features;
using EasyNetQ;
using OrderMS.App.Domain;
using OrderMS.App.Infrastructure.Interface;
using Shared.RabbitMQ.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrderMS.App.Web.QueueHandler
{
    public class ProductUpdatedMessageHandlerService : BackgroundService
    {
        private readonly IBus _bus;
        private SubscriptionResult _subscriptionResult;
        private readonly IService<ProductItem> _productService;
        private readonly IMapper _mapper;

        public ProductUpdatedMessageHandlerService(IBus bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        public override async Task<Task> StartAsync(CancellationToken cancellationToken)
        {
            _subscriptionResult  = await _bus.PubSub.SubscribeAsync<ProductUpdatedMessage>("OrderMS", async message =>
            {
                if (message.EventType == EventType.Create)
                {
                    await _productService.CreateAsync(_mapper.Map<ProductItem>(message));
                }
                else if (message.EventType == EventType.Update) 
                {
                    await _productService.UpdateAsync(_mapper.Map<ProductItem>(message));
                }
                else if (message.EventType == EventType.Delete)
                {
                    await _productService.DeleteAsync(message.ProductId);
                }
            });

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _subscriptionResult.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}
