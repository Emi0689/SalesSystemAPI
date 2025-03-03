using SalesSystem.DTO;

namespace SalesSystem.BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAll();
        Task<ProductDTO> Create(ProductDTO ProductDTO);
        Task<bool> Update(ProductDTO ProductDTO);
        Task<bool> Delete(int id);
    }
}
