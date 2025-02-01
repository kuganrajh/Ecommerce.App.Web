using OrderMS.App.BLL;
using OrderMS.App.Domain;
using OrderMS.App.Infrastructure.Interface;

namespace OrderMS.App.Web.GraphQL.Mutation
{
    public class OrderMutation
    {
        public async Task<Order> CreateOrder(Order order, [Service] IService<Order> orderService)
        {
            return await orderService.CreateAsync(order);
        }

        public async Task<Order> UpdateOrder(Order order, [Service] IService<Order> orderService)
        {
            return await orderService.UpdateAsync(order);
        }

        public async Task<bool> DeleteOrder(Guid id, [Service] IService<Order> orderService)
        {
            return await orderService.DeleteAsync(id);
        }
    }
}
