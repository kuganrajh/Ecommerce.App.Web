using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.App.Domain;
using Product.App.Infrastructure.Interface;

namespace Product.App.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IService<ProductItem> _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IService<ProductItem> productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll method invoked");
            try
            {
                _logger.LogDebug("Fetching all products from the service.");
                var products = await _productService.GetAsync();

                if (products == null || !products.Any())
                {
                    _logger.LogWarning("No products found in the database.");
                    return NotFound("No products available.");
                }

                _logger.LogInformation("Successfully retrieved {Count} products.", products.Count);
                return Ok(products); // JSON response by default
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching products at {Timestamp}.", DateTime.UtcNow);
                return StatusCode(500, "An error occurred while retrieving products. Please try again later.");
            }
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _productService.GetAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product); // JSON response
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductItem product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdProduct = await _productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.ProductId }, createdProduct); // JSON response
        }

        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductItem product)
        {
            if (id != product.ProductId)
                return BadRequest("Product ID mismatch");

            var updatedProduct = await _productService.UpdateAsync(product);
            return Ok(updatedProduct); // JSON response
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _productService.DeleteAsync(id);
            if (!isDeleted)
                return NotFound();

            return NoContent(); // No response body
        }
    }
}
