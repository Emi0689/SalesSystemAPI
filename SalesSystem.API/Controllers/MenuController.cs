using SalesSystem.API.Utilities;
using SalesSystem.DTO;
using SalesSystem.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.BLL.Services;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(int userId)
        {
            var rsp = new Response<List<MenuDTO>>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _menuService.GetMenuAsync(userId);
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
