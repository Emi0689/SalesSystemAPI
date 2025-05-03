using SalesSystem.BLL.Services.Interfaces;
using AutoMapper;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace SalesSystem.BLL.Services
{
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<MenuRol> _menuRolRepository;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<Menu> menuRepository, 
            IGenericRepository<User> userRepository, 
            IGenericRepository<MenuRol> menuRolRepository, 
            IMapper mapper)
        {
            _menuRepository = menuRepository;
            _userRepository = userRepository;
            _menuRolRepository = menuRolRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> GetMenu(int id)
        {
            IQueryable<User> user =  _userRepository.GetAll(u => u.IdUser == id);
            IQueryable<MenuRol> menuRols =  _menuRolRepository.GetAll();
            IQueryable<Menu> menu =  _menuRepository.GetAll();

            try
            {
                IQueryable<Menu> menuResult = (from u in user 
                                         join mr in menuRols on u.IdRol equals mr.IdRol
                                         join m in menu on mr.IdMenu equals m.IdMenu
                                         select m);

                var result = await menuResult.ToListAsync();
                return _mapper.Map<List<MenuDTO>>(result);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
