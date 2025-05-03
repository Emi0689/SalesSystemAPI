using SalesSystem.DTO;


namespace SalesSystem.BLL.Services.Interfaces
{
    public interface ISaleService
    {
        Task<SaleDTO> CreateAsync(SaleDTO SaleDTO);
        Task<List<SaleDTO>> HistoryAsync(string searchFor, string saleNumber, string startDate, string endDate);
        Task<List<ReportDTO>> GetReportAsync(string startDate, string endDate);
    }
}
