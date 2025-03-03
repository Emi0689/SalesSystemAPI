using SalesSystem.API.Utilities;
using SalesSystem.DTO;
using SalesSystem.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.BLL.Services;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var rsp = new Response<List<CategoryDTO>>();
            try
            {
                rsp.status = true;
                rsp.value = await _categoryService.GetAll();
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
