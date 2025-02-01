using AutoMapper;
using OrderMS.App.Domain;
using OrderMS.App.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMS.App.BLL
{
    /// <summary>
    /// Service class for managing Order operations.
    /// Implements IService<Order>.
    /// </summary>
    public class OrderService : IService<Order>
    {
        private readonly IRepository<Order> _orderRepository;
        // private readonly IRabbitMQService<OrderUpdatedMessage> _rabbitMQService;
        //private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the OrderService class.
        /// </summary>
        public OrderService(IRepository<Order> orderRepository /*, IRabbitMQService<OrderUpdatedMessage> rabbitMQService, */ /*IMapper mapper*/)
        {
            _orderRepository = orderRepository;
            // _rabbitMQService = rabbitMQService;
            //_mapper = mapper;
        }

        /// <summary>
        /// Get all orders from the repository.
        /// </summary>
        /// <returns>List of all orders.</returns>
        public async Task<List<Order>> GetAsync()
        {
            var listData = await _orderRepository.GetAsync();
            return listData;
        }

        /// <summary>
        /// Get a specific order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order.</param>
        /// <returns>The order entity if found.</returns>
        public async Task<Order> GetAsync(Guid id)
        {
            var data = await _orderRepository.GetAsync(id);
            return data;
        }

        /// <summary>
        /// Save a new order to the repository.
        /// </summary>
        /// <param name="order">The order entity to save.</param>
        /// <returns>The created order entity.</returns>
        public async Task<Order> CreateAsync(Order order)
        {
            var data = await _orderRepository.CreateAsync(order);

            // AutoMapper mapping
            // var orderUpdatedMessage = _mapper.Map<OrderUpdatedMessage>(data);
            // orderUpdatedMessage.EventType = EventType.Create;
            // _rabbitMQService.PublishMessage(orderUpdatedMessage);

            return data;
        }

        /// <summary>
        /// Update an existing order in the repository.
        /// </summary>
        /// <param name="order">The order entity with updated values.</param>
        /// <returns>The updated order entity.</returns>
        public async Task<Order> UpdateAsync(Order order)
        {
            var data = await _orderRepository.UpdateAsync(order);

            // AutoMapper mapping
            // var orderUpdatedMessage = _mapper.Map<OrderUpdatedMessage>(data);
            // orderUpdatedMessage.EventType = EventType.Update;
            // _rabbitMQService.PublishMessage(orderUpdatedMessage);

            return data;
        }

        /// <summary>
        /// Delete an order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        /// <returns>True if the order was successfully deleted.</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            bool data = await _orderRepository.DeleteAsync(id);

            // AutoMapper mapping
            // var orderUpdatedMessage = new OrderUpdatedMessage() { OrderId = id, EventType = EventType.Delete };
            // _rabbitMQService.PublishMessage(orderUpdatedMessage);

            return data;
        }
    }
}
