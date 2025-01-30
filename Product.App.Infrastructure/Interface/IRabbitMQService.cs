using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Product.App.Infrastructure.Interface
{
    public interface IRabbitMQService<T>
    {
        void PublishMessage(T model);

        void SubscribeToMessages();
    }
}
