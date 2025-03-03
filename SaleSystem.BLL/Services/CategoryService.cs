using AutoMapper;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model.Entities;

namespace SalesSystem.BLL.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GetAll()
        {
            try
            {
                var categories = await _categoryRepository.GetAll();
                return _mapper.Map<List<CategoryDTO>>(categories.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
