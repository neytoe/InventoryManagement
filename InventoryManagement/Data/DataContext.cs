using InventoryManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Inventory> Inventories { get; set; } 
        public DbSet<Product> Products { get; set; }

    }
}
