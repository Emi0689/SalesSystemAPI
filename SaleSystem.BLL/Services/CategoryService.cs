using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model.Entities;

namespace SalesSystem.BLL.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryGenRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _categoryGenRepo = _unitOfWork.GetGenRepo<Category>();
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            try
            {
                return _mapper.Map<List<CategoryDTO>>(await _categoryGenRepo.GetAllAsync());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
