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
                rsp.status = true;
                rsp.value = await _menuService.GetMenu(userId);
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
