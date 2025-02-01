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
    /// Service class for managing ProductItem operations.
    /// Implements IService<ProductItem>.
    /// </summary>
    public class ProductItemService : IService<ProductItem>
    {
        private readonly IRepository<ProductItem> _productItemRepository;

        /// <summary>
        /// Initializes a new instance of the ProductItemService class.
        /// </summary>
        public ProductItemService(IRepository<ProductItem> productItemRepository)
        {
            _productItemRepository = productItemRepository;
        }

        /// <summary>
        /// Get all product items from the repository.
        /// </summary>
        /// <returns>List of all product items.</returns>
        public async Task<List<ProductItem>> GetAsync()
        {
            var listData = await _productItemRepository.GetAsync();
            return listData;
        }

        /// <summary>
        /// Get a specific product item by its ID.
        /// </summary>
        /// <param name="id">The ID of the product item.</param>
        /// <returns>The product item entity if found.</returns>
        public async Task<ProductItem> GetAsync(Guid id)
        {
            var data = await _productItemRepository.GetAsync(id);
            return data;
        }

        /// <summary>
        /// Save a new product item to the repository.
        /// </summary>
        /// <param name="productItem">The product item entity to save.</param>
        /// <returns>The created product item entity.</returns>
        public async Task<ProductItem> CreateAsync(ProductItem productItem)
        {
            var data = await _productItemRepository.CreateAsync(productItem);
            return data;
        }

        /// <summary>
        /// Update an existing product item in the repository.
        /// </summary>
        /// <param name="productItem">The product item entity with updated values.</param>
        /// <returns>The updated product item entity.</returns>
        public async Task<ProductItem> UpdateAsync(ProductItem productItem)
        {
            var data = await _productItemRepository.UpdateAsync(productItem);
            return data;
        }

        /// <summary>
        /// Delete a product item by its ID.
        /// </summary>
        /// <param name="id">The ID of the product item to delete.</param>
        /// <returns>True if the product item was successfully deleted.</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            bool data = await _productItemRepository.DeleteAsync(id);
            return data;
        }
    }

}
