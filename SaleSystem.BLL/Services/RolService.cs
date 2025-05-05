using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model.Entities;

namespace SalesSystem.BLL.Services
{
    public class RolService: IRolService
    {
        private readonly IGenericRepository<Rol> _rolGenRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RolService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _rolGenRepo = _unitOfWork.GetGenRepo<Rol>();
            _mapper = mapper;
        }

        public async Task<List<RolDTO>> GetAll()
        {
            try
            {
                return _mapper.Map<List<RolDTO>>(await _rolGenRepo.GetAllAsync());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
