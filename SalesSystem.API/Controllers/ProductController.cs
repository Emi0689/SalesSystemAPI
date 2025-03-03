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
                rsp.status = true;
                rsp.value = await _productService.GetAll();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.message = ex.Message;
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
                rsp.status = true;
                rsp.value = await _productService.Create(productDTO);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.message = ex.Message;
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
                rsp.status = true;
                rsp.value = await _productService.Update(productDTO);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.message = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var rsp = new Response<bool>();
            try
            {
                rsp.status = true;
                rsp.value = await _productService.Delete(productId);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.message = ex.Message;
            }
            return Ok(rsp);
        }
    }
}
