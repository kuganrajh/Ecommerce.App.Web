﻿using AutoMapper;
using Product.App.Domain;
using Product.App.Infrastructure.Interface;
using Shared.RabbitMQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Product.App.BLL
{
    // <summary>
    /// Service class for managing Product operations.
    /// Implements IService<Product>.
    /// </summary>
    public class ProductService : IService<ProductItem>
    {
        private readonly IRepository<ProductItem> _productRepository;
        private readonly IRabbitMQService<ProductUpdatedMessage> _rabbitMQService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the ProductService class.
        /// </summary>
        public ProductService(IRepository<ProductItem> productRepository, IRabbitMQService<ProductUpdatedMessage> rabbitMQService, IMapper mapper)
        {
            _productRepository = productRepository;
            _rabbitMQService = rabbitMQService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all products from the repository.
        /// </summary>
        /// <returns>List of all products.</returns>
        public Task<List<ProductItem>> GetAsync()
        {
            var listData = _productRepository.GetAsync();
            return listData;
        }

        /// <summary>
        /// Get a specific product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product entity if found.</returns>
        public async Task<ProductItem> GetAsync(Guid id)
        {
            var data = await _productRepository.GetAsync(id);
            return data;
        }

        /// <summary>
        /// Save a new product to the repository.
        /// </summary>
        /// <param name="product">The product entity to save.</param>
        /// <returns>The created product entity.</returns>
        public async Task<ProductItem> CreateAsync(ProductItem product)
        {
            var data = await _productRepository.CreateAsync(product);

            // AutoMapper mapping
            var productUpdatedMessage = _mapper.Map<ProductUpdatedMessage>(data);
            productUpdatedMessage.EventType = EventType.Create;
            _rabbitMQService.PublishMessage(productUpdatedMessage);

            return data;
        }

        /// <summary>
        /// Update an existing product in the repository.
        /// </summary>
        /// <param name="product">The product entity with updated values.</param>
        /// <returns>The updated product entity.</returns>
        public async Task<ProductItem> UpdateAsync(ProductItem product)
        {
            var data = await _productRepository.UpdateAsync(product);

            // AutoMapper mapping
            var productUpdatedMessage = _mapper.Map<ProductUpdatedMessage>(data);
            productUpdatedMessage.EventType = EventType.Update;
            _rabbitMQService.PublishMessage(productUpdatedMessage);

            return data;
        }

        /// <summary>
        /// Delete a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>The ID of the deleted product.</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            bool data = await _productRepository.DeleteAsync(id);

            // AutoMapper mapping
            var productUpdatedMessage = new ProductUpdatedMessage() { ProductId = id,EventType = EventType.Delete};
            _rabbitMQService.PublishMessage(productUpdatedMessage);

            return data;
        }
    }
}
