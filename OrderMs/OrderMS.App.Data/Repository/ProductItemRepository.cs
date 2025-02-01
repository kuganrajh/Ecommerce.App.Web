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
    public class ProductItemRepository : IRepository<ProductItem>
    {
        private readonly OrderDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the ProductItemRepository class.
        /// </summary>
        /// <param name="dbContext">The database context for Product entities.</param>
        public ProductItemRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Creates a new product in the database.
        /// </summary>
        /// <param name="product">The product entity to create.</param>
        /// <returns>The created product entity.</returns>
        public async Task<ProductItem> CreateAsync(ProductItem product)
        {
            var data = await _dbContext.ProductItems.AddAsync(product); // Add the product to the DbContext
            await _dbContext.SaveChangesAsync(); // Save changes to the database
            return data.Entity; // Return the created product entity
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>True if the product was successfully deleted, false if not found.</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _dbContext.ProductItems.FindAsync(id); // Find the product by ID
            if (product == null)
            {
                return false; // Return false if the product is not found
            }

            _dbContext.ProductItems.Remove(product); // Remove the product from the DbContext
            await _dbContext.SaveChangesAsync(); // Save changes to the database
            return true; // Return true to indicate successful deletion
        }

        /// <summary>
        /// Retrieves all products from the database.
        /// </summary>
        /// <returns>A list of all products.</returns>
        public async Task<List<ProductItem>> GetAsync() => await _dbContext.ProductItems.ToListAsync(); // Return a list of all products

        /// <summary>
        /// Retrieves a product by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product entity if found, otherwise null.</returns>
        public async Task<ProductItem> GetAsync(Guid id) => await _dbContext.ProductItems.FindAsync(id); // Return the product or null if not found

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        /// <param name="product">The product entity with updated values.</param>
        /// <returns>The updated product entity.</returns>
        public async Task<ProductItem> UpdateAsync(ProductItem product)
        {
            _dbContext.Entry(product).State = EntityState.Modified; // Mark the product entity as modified
            await _dbContext.SaveChangesAsync(); // Save changes to the database
            return product; // Return the updated product entity
        }
    }


}
