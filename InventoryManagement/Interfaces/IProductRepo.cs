using InventoryManagement.Entities;

namespace InventoryManagement.Interfaces
{
    public interface IProductRepo : IRepository<Product>
    {
        Task<IEnumerable<Product>> FindAllProductswithinventory(int id);
        Task<Product> FindProductbyId(int id);
        Task<IEnumerable<Product>> FindDeletedProductsById(int id);
        Task<Product> FindDeletedProductById(int id);
    }
    
}
