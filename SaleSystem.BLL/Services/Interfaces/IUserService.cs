using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
