using SalesSystem.DTO;

namespace SalesSystem.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAll();
        Task<SessionDTO> ValidateCredentials(string email, string password);
        Task<UserDTO> Create(UserDTO userDTO);
        Task<bool> Update(UserDTO userDTO);
        Task<bool> Delete(int id);
    }
}
