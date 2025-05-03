using SalesSystem.DTO;

namespace SalesSystem.BLL.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDTO> GetResumeAsync();
    }
}
