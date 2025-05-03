using SalesSystem.DTO;

namespace SalesSystem.BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllAsync();
        Task<ProductDTO> CreateAsync(ProductDTO ProductDTO);
        Task<bool> UpdateAsync(ProductDTO ProductDTO);
        Task<bool> DeleteAsync(int id);
    }
}
