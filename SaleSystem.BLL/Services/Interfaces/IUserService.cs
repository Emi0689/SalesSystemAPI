using SalesSystem.DTO;

namespace SalesSystem.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllAsync();
        Task<SessionDTO> ValidateCredentialsAsync(string email, string password);
        Task<UserDTO> CreateAsync(UserDTO userDTO);
        Task<bool> UpdateAsync(UserDTO userDTO);
        Task<bool> DeleteAsync(int id);
    }
}
