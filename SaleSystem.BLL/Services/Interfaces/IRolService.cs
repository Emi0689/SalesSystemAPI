using SalesSystem.DTO;

namespace SalesSystem.BLL.Services.Interfaces
{
    public interface IRolService
    {
        Task<List<RolDTO>> GetAllAsync();
    }
}
