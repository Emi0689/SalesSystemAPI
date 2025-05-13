using SalesSystem.API.Common;
using SalesSystem.DTO;
using SalesSystem.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>List of products.</returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();

            return Ok(new Response<List<ProductDTO>>
            {
                Success = true,
                Value = products
            });
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productDTO">Product data transfer object.</param>
        /// <returns>201 Created with the created product.</returns>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] ProductDTO productDTO)
        {
            var createdProduct = await _productService.CreateAsync(productDTO);

            return Created(uri: Url.Action(nameof(Create), createdProduct.IdProduct), value: new Response<ProductDTO>
            {
                Success = true,
                Value = createdProduct
            });
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="productDTO">Product data transfer object.</param>
        /// <returns>204 No Content.</returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] ProductDTO productDTO)
        {
            await _productService.UpdateAsync(productDTO);

            return NoContent();
        }

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="productId">The ID of the product to delete.</param>
        /// <returns>200 OK with a success response.</returns>
        [HttpDelete]
        [Route("Delete/{productId:int}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var result = await _productService.DeleteAsync(productId);

            return Ok(new Response<bool>
            {
                Success = true,
                Value = result
            });
        }
    }
}
