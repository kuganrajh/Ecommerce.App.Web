
namespace Product.App.Infrastructure.Interface
{
    public interface IRabbitMQService<T>
    {
        void PublishMessage(T model);

        void SubscribeToMessages();
    }
}
