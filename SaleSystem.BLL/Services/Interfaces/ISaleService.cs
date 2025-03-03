using SalesSystem.DTO;


namespace SalesSystem.BLL.Services.Interfaces
{
    public interface ISaleService
    {
        Task<SaleDTO> Create(SaleDTO SaleDTO);
        Task<List<SaleDTO>> History(string searchFor, string saleNumber, string startDate, string endDate);
        Task<List<ReportDTO>> Report(string startDate, string endDate);
    }
}
