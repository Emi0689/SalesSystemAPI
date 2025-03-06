using SalesSystem.API.Utilities;
using SalesSystem.DTO;
using SalesSystem.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.BLL.Services;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] SaleDTO saleDTO)
        {
            var rsp = new Response<SaleDTO>();
            try
            {
                rsp.status = true;
                rsp.value = await _saleService.Create(saleDTO);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.message = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpGet]
        [Route("History")]
        public async Task<IActionResult> History(string searchFor, string? saleNumber, string? startDate, string? endDate)
        {
            var rsp = new Response<List<SaleDTO>>();
            try
            {
                rsp.status = true;
                rsp.value = await _saleService.History(searchFor, saleNumber, startDate, endDate);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.message = ex.Message;
            }
            return Ok(rsp);
        }


        [HttpGet]
        [Route("Report")]
        public async Task<IActionResult> Report(string startDate, string endDate)
        {
            var rsp = new Response<List<ReportDTO>>();
            try
            {
                rsp.status = true;
                rsp.value = await _saleService.Report(startDate, endDate);
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
