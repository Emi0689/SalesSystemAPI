using SalesSystem.API.Utilities;
using SalesSystem.DTO;
using SalesSystem.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.BLL.Services;

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

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var rsp = new Response<List<ProductDTO>>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _productService.GetAllAsync();
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.ErrorMessage = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] ProductDTO productDTO)
        {
            var rsp = new Response<ProductDTO>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _productService.CreateAsync(productDTO);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.ErrorMessage = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] ProductDTO productDTO)
        {
            var rsp = new Response<bool>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _productService.UpdateAsync(productDTO);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.ErrorMessage = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpDelete]
        [Route("Delete/{productId:int}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var rsp = new Response<bool>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _productService.DeleteAsync(productId);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.ErrorMessage = ex.Message;
            }
            return Ok(rsp);
        }
    }
}
