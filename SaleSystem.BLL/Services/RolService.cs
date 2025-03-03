using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model.Entities;

namespace SalesSystem.BLL.Services
{
    public class RolService: IRolService
    {
        private readonly IGenericRepository<Rol> _rolRepository;
        private readonly IMapper _mapper;

        public RolService(IGenericRepository<Rol> rolRepository, IMapper mapper)
        {
            _rolRepository = rolRepository;
            _mapper = mapper;
        }

        public async Task<List<RolDTO>> GetAll()
        {
            try
            {
                var rols = await _rolRepository.GetAll();
                return _mapper.Map<List<RolDTO>>(rols.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
