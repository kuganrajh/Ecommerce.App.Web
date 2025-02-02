using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMS.App.Infrastructure.Interface
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event, string topicName);
    }
}
