using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model.Entities;
using SalesSystem.Utility;

namespace SalesSystem.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync(includes: [user => user.Include(rol => rol.IdRolNavigation)]);
                return _mapper.Map<List<UserDTO>>(users);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDTO> CreateAsync(UserDTO userDTO)
        {
            try
            {
                var userCreated = await _userRepository.CreateAsync(_mapper.Map<User>(userDTO));
                if (userCreated.IdUser == 0)
                {
                    throw new TaskCanceledException("The user does not exist.");
                }
                userCreated = await _userRepository.GetSingleAsync(u => u.IdUser == userCreated.IdUser,
                                                                  [user => user.Include(rol => rol.IdRolNavigation)]);
                return _mapper.Map<UserDTO>(userCreated);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var userFound = await _userRepository.GetSingleAsync(u => u.IdUser == id);

                if (userFound?.IdRol != Constants.rolAdmin)
                    throw new Exception("Nop!");

                if (userFound.IdUser == 0)
                {
                    throw new TaskCanceledException("The user does not exist");
                }
                bool result = await _userRepository.DeleteAsync(userFound);
                if (!result)
                {
                    throw new TaskCanceledException("The user could not be deleted.");
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                var userFound = await _userRepository.GetSingleAsync(u => u.IdUser == userDTO.IdUser);
                if (userFound?.IdUser == 0)
                {
                    throw new TaskCanceledException("The user does not exist.");
                }
                userFound.FullName = user.FullName;
                userFound.Email = user.Email;
                userFound.IdRol = user.IdRol;
                userFound.IsActive = user.IsActive;
                userFound.Password = user.Password;

                bool result = await _userRepository.UpdateAsync(userFound);

                if (!result)
                {
                    throw new TaskCanceledException("The user could not be updated.");
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SessionDTO> ValidateCredentialsAsync(string email, string password)
        {
            try
            {
                var user = await _userRepository.GetSingleAsync(u => u.Email == email && u.Password == password,
                    [user => user.Include(rol => rol.IdRolNavigation)]);


                if (user == null)
                {
                    throw new TaskCanceledException("The user does not exist.");
                }

                return _mapper.Map<SessionDTO>(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
