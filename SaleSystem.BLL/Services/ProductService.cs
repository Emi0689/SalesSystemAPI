using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model.Entities;

namespace SalesSystem.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> ProductRepository, IMapper mapper)
        {
            _productRepository = ProductRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> GetAll()
        {
            try
            {
                var productQuery = await _productRepository.GetAll();
                var products = productQuery.Include(rol => rol.IdCategoryNavigation).ToList();
                return _mapper.Map<List<ProductDTO>>(products);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<ProductDTO> Create(ProductDTO ProductDTO)
        {
            try
            {
                var productCreated = await _productRepository.Create(_mapper.Map<Product>(ProductDTO));
                if (productCreated.IdProduct == 0)
                {
                    throw new TaskCanceledException("The product does not exist");
                }
                var query = await _productRepository.GetAll(u => u.IdProduct == productCreated.IdProduct);
                productCreated = query.Include(rol => rol.IdCategoryNavigation).First();
                return _mapper.Map<ProductDTO>(productCreated);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(ProductDTO ProductDTO)
        {
            try
            {
                var product = _mapper.Map<Product>(ProductDTO);
                var productFound = await _productRepository.Get(u => u.IdProduct == ProductDTO.IdProduct);
                if (productFound.IdProduct == 0)
                {
                    throw new TaskCanceledException("The Product does not exist");
                }
                productFound.Price = product.Price;
                productFound.Stock = product.Stock;
                productFound.IdCategory = product.IdCategory;
                productFound.IsActive = product.IsActive;
                productFound.Name = product.Name;

                bool result = await _productRepository.Update(productFound);

                if (!result)
                {
                    throw new TaskCanceledException("The product could not be updated");
                }
                return result;
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
                var productFound = await _productRepository.Get(u => u.IdProduct == id);
                if (productFound.IdProduct == 0)
                {
                    throw new TaskCanceledException("The product does not exist");
                }
                bool result = await _productRepository.Delete(productFound);
                if (result)
                {
                    throw new TaskCanceledException("The product could not be updated");
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
