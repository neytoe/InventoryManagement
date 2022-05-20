using InventoryManagement.Data;
using InventoryManagement.Entities;
using InventoryManagement.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class ProductRepo : BaseRepository<Product>, IProductRepo
    {
        public ProductRepo(DataContext dataContext) : base(dataContext)
        {
        }
        public async Task<Product> FindProductbyId(int id)
        {
            return await _dataContext.Products.Where(x => x.IsDeleted == false && x.Id == id).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<Product>> FindAllProductswithinventory( int Id)
        {
            return await _dataContext.Products.Where(x => x.IsDeleted == false && x.InventoryId == Id).ToListAsync();
          

        }

        public async Task<IEnumerable<Product>> FindDeletedProductsById(int id)
        {
            return await _dataContext.Products.Where(x => x.IsDeleted == true && x.InventoryId == id).ToListAsync();
        }

        public async Task<Product> FindDeletedProductById(int id)
        {
            return await _dataContext.Products.Where(x => x.IsDeleted == true && x.Id == id).FirstOrDefaultAsync();
        }
    }
}
