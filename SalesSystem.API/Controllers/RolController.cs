using Microsoft.AspNetCore.Mvc;
using SalesSystem.API.Common;
using SalesSystem.DTO;
using SalesSystem.BLL.Services.Interfaces;


namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        /// <summary>
        /// Retrieves all available roles.
        /// </summary>
        /// <returns>List of roles.</returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _rolService.GetAll();

            return Ok(new Response<List<RolDTO>>
            {
                Success = true,
                Value = roles
            });
        }
    }
}
