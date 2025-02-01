using OrderMS.App.BLL;
using OrderMS.App.Domain;
using OrderMS.App.Infrastructure.Interface;

namespace OrderMS.App.Web.GraphQL.Query
{
    public class OrderQuery
    {
        [GraphQLName("getOrders")]
        [UseFiltering]
        public async Task<IQueryable<Order>> GetOrders([Service] IService<Order> orderService)
        {
            var orders = await orderService.GetAsync();
            return orders.AsQueryable();
        }

        [GraphQLName("getOrderById")]
        public async Task<Order> GetOrderById(Guid id, [Service] IService<Order> orderService)
        {
            return await orderService.GetAsync(id);
        }

        public string HealthCheck() => "GraphQL is working!";

    }
}
