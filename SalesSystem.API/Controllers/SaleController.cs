using SalesSystem.API.Common;
using SalesSystem.DTO;
using SalesSystem.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// Creates a new sale.
        /// </summary>
        /// <param name="saleDTO">Sale data to create.</param>
        /// <returns>201 Created with the created sale.</returns>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] SaleDTO saleDTO)
        {
            var createdSale = await _saleService.Create(saleDTO);

            return Created(
                uri: Url.Action(nameof(Create), new { id = createdSale.IdSale }),
                value: new Response<SaleDTO>
                {
                    Success = true,
                    Value = createdSale
                });
        }

        /// <summary>
        /// Retrieves sales history based on filters.
        /// </summary>
        /// <param name="searchFor">Search term (client or product).</param>
        /// <param name="saleNumber">Optional sale number filter.</param>
        /// <param name="startDate">Start date (yyyy-MM-dd).</param>
        /// <param name="endDate">End date (yyyy-MM-dd).</param>
        /// <returns>List of sales matching the filters.</returns>
        [HttpGet]
        [Route("History")]
        public async Task<IActionResult> History(string searchFor, string? saleNumber, string? startDate, string? endDate)
        {
            var history = await _saleService.History(searchFor, saleNumber, startDate, endDate);

            return Ok(new Response<List<SaleDTO>>
            {
                Success = true,
                Value = history
            });
        }


        /// <summary>
        /// Generates a sales report between two dates.
        /// </summary>
        /// <param name="startDate">Start date (yyyy-MM-dd).</param>
        /// <param name="endDate">End date (yyyy-MM-dd).</param>
        /// <returns>List of report entries.</returns>
        [HttpGet]
        [Route("Report")]
        public async Task<IActionResult> Report(string startDate, string endDate)
        {
            var report = await _saleService.Report(startDate, endDate);

            return Ok(new Response<List<ReportDTO>>
            {
                Success = true,
                Value = report
            });
        }
    }
}
