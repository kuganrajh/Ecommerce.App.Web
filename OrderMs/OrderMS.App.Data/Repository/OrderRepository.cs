using Microsoft.EntityFrameworkCore;
using OrderMS.App.Data.Context;
using OrderMS.App.Domain;
using OrderMS.App.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMS.App.Data.Repository
{
    /// <summary>
    /// Repository class for managing Order entities in the database.
    /// Implements IRepository<Order> interface for CRUD operations.
    /// </summary>
    public class OrderRepository : IRepository<Order>
    {
        private readonly OrderDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the OrderRepository class.
        /// </summary>
        /// <param name="dbContext">The database context for Order entities.</param>
        public OrderRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Creates a new order in the database.
        /// </summary>
        /// <param name="order">The order entity to create.</param>
        /// <returns>The created order entity.</returns>
        public async Task<Order> CreateAsync(Order order)
        {
            var data = await _dbContext.Orders.AddAsync(order); // Add the order to the DbContext
            await _dbContext.SaveChangesAsync(); // Save changes to the database
            return data.Entity; // Return the created order entity
        }

        /// <summary>
        /// Deletes an order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        /// <returns>True if the order was successfully deleted, false if not found.</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var order = await _dbContext.Orders.FindAsync(id); // Find the order by ID
            if (order == null)
            {
                return false; // Return false if the order is not found
            }

            _dbContext.Orders.Remove(order); // Remove the order from the DbContext
            await _dbContext.SaveChangesAsync(); // Save changes to the database
            return true; // Return true to indicate successful deletion
        }

        /// <summary>
        /// Retrieves all orders from the database.
        /// </summary>
        /// <returns>A list of all orders.</returns>
        public async Task<List<Order>> GetAsync() => await _dbContext.Orders.ToListAsync(); // Return a list of all orders

        /// <summary>
        /// Retrieves an order by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>The order entity if found, otherwise null.</returns>
        public async Task<Order> GetAsync(Guid id) => await _dbContext.Orders.FindAsync(id); // Return the order or null if not found

        /// <summary>
        /// Updates an existing order in the database.
        /// </summary>
        /// <param name="order">The order entity with updated values.</param>
        /// <returns>The updated order entity.</returns>
        public async Task<Order> UpdateAsync(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified; // Mark the order entity as modified
            await _dbContext.SaveChangesAsync(); // Save changes to the database
            return order; // Return the updated order entity
        }
    }
}
