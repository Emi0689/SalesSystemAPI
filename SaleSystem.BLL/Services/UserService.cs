using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model.Entities;
using SalesSystem.Utility;
using System.Reflection.Metadata;

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
                var userQuery =  _userRepository.GetAll();
                var users = await userQuery.Include(rol => rol.IdRolNavigation).ToListAsync();
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
                    throw new TaskCanceledException("The user does not exist.");
                }
                var query =  _userRepository.GetAll(u => u.IdUser == userCreated.IdUser);
                userCreated = await query.Include(rol => rol.IdRolNavigation).FirstAsync();
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

                if (userFound.IdRol != Constants.rolAdmin)
                    throw new Exception("Nop!");

                if (userFound.IdUser == 0)
                {
                    throw new TaskCanceledException("The user does not exist");
                }
                bool result = await _userRepository.Delete(userFound);
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

        public async Task<bool> Update(UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                var userFound = await _userRepository.Get(u => u.IdUser == userDTO.IdUser);
                if (userFound.IdUser == 0)
                {
                    throw new TaskCanceledException("The user does not exist.");
                }
                userFound.FullName = user.FullName;
                userFound.Email = user.Email;
                userFound.IdRol = user.IdRol;
                userFound.IsActive = user.IsActive;
                userFound.Password = user.Password;

                bool result = await _userRepository.Update(userFound);

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

        public async Task<SessionDTO> ValidateCredentials(string email, string password)
        {
            try
            {
                var userQuery =  _userRepository.GetAll(u => u.Email == email && u.Password == password);

                User user = await userQuery.Include(rol => rol.IdRolNavigation).FirstAsync();

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
