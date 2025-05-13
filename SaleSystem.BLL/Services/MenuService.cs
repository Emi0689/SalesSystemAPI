using SalesSystem.BLL.Services.Interfaces;
using AutoMapper;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using SalesSystem.DAL.Repositories;

namespace SalesSystem.BLL.Services
{
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<Menu> _menuGenRepo;
        private readonly IGenericRepository<User> _userGenRepo;
        private readonly IGenericRepository<MenuRol> _menuRolGenRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MenuService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _menuGenRepo = _unitOfWork.GetGenRepo<Menu>();
            _userGenRepo = _unitOfWork.GetGenRepo<User>();
            _menuRolGenRepo = _unitOfWork.GetGenRepo<MenuRol>();
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> GetMenu(int id)
        {
            IQueryable<User> user =  _userGenRepo.GetQuery(u => u.IdUser == id);
            IQueryable<MenuRol> menuRols =  _menuRolGenRepo.GetQuery();
            IQueryable<Menu> menu =  _menuGenRepo.GetQuery();

            IQueryable<Menu> menuResult = (from u in user 
                                        join mr in menuRols on u.IdRol equals mr.IdRol
                                        join m in menu on mr.IdMenu equals m.IdMenu
                                        select m);

            var result = await menuResult.ToListAsync();
            return _mapper.Map<List<MenuDTO>>(result);
        }
    }
}
