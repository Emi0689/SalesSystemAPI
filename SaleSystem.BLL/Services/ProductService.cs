using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model.Entities;

namespace SalesSystem.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IGenericRepository<Product> _productGenRepo;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this._productGenRepo = _unitOfWork.GetGenRepo<Product>();
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            try
            {
                var productQuery = _productGenRepo.GetAllAsync();
                var productQueryWithRol = productQuery.Include(rol => rol.IdCategoryNavigation);
                var products = await productQueryWithRol.ToListAsync();
                return _mapper.Map<List<ProductDTO>>(products);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<ProductDTO> CreateAsync(ProductDTO ProductDTO)
        {
            try
            {
                var productCreated = await _productGenRepo.CreateAsync(_mapper.Map<Product>(ProductDTO));
                if (productCreated.IdProduct == 0)
                {
                    throw new TaskCanceledException("The product does not exist.");
                }
                var query = _productGenRepo.GetAllAsync(u => u.IdProduct == productCreated.IdProduct);
                productCreated = await query.Include(rol => rol.IdCategoryNavigation).FirstAsync();
                return _mapper.Map<ProductDTO>(productCreated);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(ProductDTO ProductDTO)
        {
            try
            {
                var product = _mapper.Map<Product>(ProductDTO);
                var productFound = await _productGenRepo.GetAsync(u => u.IdProduct == ProductDTO.IdProduct);
                if (productFound?.IdProduct == 0)
                {
                    throw new TaskCanceledException("The Product does not exist.");
                }
                productFound.Price = product.Price;
                productFound.Stock = product.Stock;
                productFound.IdCategory = product.IdCategory;
                productFound.IsActive = product.IsActive;
                productFound.Name = product.Name;

                bool result = await _productGenRepo.UpdateAsync(productFound);

                if (!result)
                {
                    throw new TaskCanceledException("The product could not be updated.");
                }
                return result;
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
                var productFound = await _productGenRepo.GetAsync(u => u.IdProduct == id);
                if (productFound.IdProduct == 0)
                {
                    throw new TaskCanceledException("The product does not exist.");
                }
                bool result = await _productGenRepo.DeleteAsync(productFound);
                if (!result)
                {
                    throw new TaskCanceledException("The product could not be updated.");
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
