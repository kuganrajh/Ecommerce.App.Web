using OrderMS.App.Domain;
using OrderMS.App.Infrastructure.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMS.App.Data.Repository
{
    public class InMemoryOrderRepository : IRepository<Order>
    {
        private readonly ConcurrentDictionary<Guid, Order> _orders = new();

        // Constructor to seed sample data
        public InMemoryOrderRepository()
        {
            SeedSampleData();
        }

        private void SeedSampleData()
        {
            var sampleOrders = new List<Order>
        {
            new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = "CUST-001",
                Status = "Pending",
                TotalAmount = 1500,
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductId = "PROD-001", Name = "Laptop", Price = 1200, Quantity = 1 },
                    new OrderItem { ProductId = "PROD-002", Name = "Mouse", Price = 50, Quantity = 2 }
                }
            },
            new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = "CUST-002",
                Status = "Completed",
                TotalAmount = 200,
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductId = "PROD-003", Name = "Keyboard", Price = 100, Quantity = 2 }
                }
            },
            new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = "CUST-003",
                Status = "Processing",
                TotalAmount = 500,
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductId = "PROD-004", Name = "Monitor", Price = 500, Quantity = 1 }
                }
            }
        };

            foreach (var order in sampleOrders)
            {
                _orders[order.Id] = order;
            }
        }

        public Task<Order> CreateAsync(Order order)
        {
            _orders[order.Id] = order;
            return Task.FromResult(order);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return Task.FromResult(_orders.TryRemove(id, out _));
        }

        public Task<List<Order>> GetAsync()
        {
            return Task.FromResult(_orders.Values.ToList());
        }

        public Task<Order> GetAsync(Guid id)
        {
            _orders.TryGetValue(id, out var order);
            return Task.FromResult(order);
        }

        public Task<Order> UpdateAsync(Order order)
        {
            _orders[order.Id] = order;
            return Task.FromResult(order);
        }
    }
}
