using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model.Entities;

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

        public async Task<List<UserDTO>> GetAll()
        {
            try
            {
                var userQuery = await _userRepository.GetAll();
                var users = userQuery.Include(rol => rol.IdRolNavigation).ToList();
                return _mapper.Map<List<UserDTO>>(users);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDTO> Create(UserDTO userDTO)
        {
            try
            {
                var userCreated = await _userRepository.Create(_mapper.Map<User>(userDTO));
                if (userCreated.IdUser == 0)
                {
                    throw new TaskCanceledException("The user does not exist");
                }
                var query = await _userRepository.GetAll(u => u.IdUser == userCreated.IdUser);
                userCreated = query.Include(rol => rol.IdRolNavigation).First();
                return _mapper.Map<UserDTO>(userCreated);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var userFound = await _userRepository.Get(u => u.IdUser == id);
                if (userFound.IdUser == 0)
                {
                    throw new TaskCanceledException("The user does not exist");
                }
                bool result = await _userRepository.Delete(userFound);
                if (result)
                {
                    throw new TaskCanceledException("The user could not be deleted");
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                var userFound = await _userRepository.Get(u => u.IdUser == userDTO.IdUser);
                if (userFound.IdUser == 0)
                {
                    throw new TaskCanceledException("The user does not exist");
                }
                userFound.FullName = user.FullName;
                userFound.Email = user.Email;
                userFound.IdRol = user.IdRol;
                userFound.IsActive = user.IsActive;

                bool result = await _userRepository.Update(userFound);

                if (result)
                {
                    throw new TaskCanceledException("The user could not be updated");
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SessionDTO> ValidateCredentials(string email, string password)
        {
            try
            {
                var userQuery = await _userRepository.GetAll(u => u.Email == email && u.Password == password);

                if (!userQuery.Any())
                {
                    throw new TaskCanceledException("The user does not exist");
                }
                User user = userQuery.Include(rol => rol.IdRolNavigation).First();
                return _mapper.Map<SessionDTO>(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
