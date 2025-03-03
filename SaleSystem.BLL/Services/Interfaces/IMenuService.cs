using SalesSystem.DTO;


namespace SalesSystem.BLL.Services.Interfaces
{
    public interface IMenuService
    {
        /// <summary>
        /// Get Menu from user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<MenuDTO>> GetMenu(int id);
    }
}
