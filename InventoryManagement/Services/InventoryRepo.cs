using InventoryManagement.Data;
using InventoryManagement.Entities;
using InventoryManagement.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class InventoryRepo : BaseRepository<Inventory>, IInventoryRepo
    {

        public InventoryRepo(DataContext dataContext) : base(dataContext)
        {
          
        }

    
      
    }
}
