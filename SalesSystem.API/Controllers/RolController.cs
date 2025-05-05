using Microsoft.AspNetCore.Mvc;
using SalesSystem.API.Utilities;
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

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        { 
            var rsp = new Response<List<RolDTO>>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _rolService.GetAll();
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
