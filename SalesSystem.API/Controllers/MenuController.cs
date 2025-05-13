using SalesSystem.API.Common;
using SalesSystem.DTO;
using SalesSystem.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// Gets the list of menus available for the specified user.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>List of menu items for the user.</returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(int userId)
        {
            var menus = await _menuService.GetMenu(userId);

            return Ok(new Response<List<MenuDTO>>
            {
                Success = true,
                Value = menus
            });
        }
    }
}
