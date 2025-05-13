using SalesSystem.API.Common;
using SalesSystem.DTO;
using SalesSystem.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Retrieves dashboard summary data.
        /// </summary>
        /// <returns>Dashboard resume with statistics and key metrics.</returns>
        [HttpGet]
        [Route("Resume")]
        public async Task<IActionResult> Resume()
        {
            var dashboardData = await _dashboardService.Resume();

            return Ok(new Response<DashboardDTO>
            {
                Success = true,
                Value = dashboardData
            });
        }   
    }
}
