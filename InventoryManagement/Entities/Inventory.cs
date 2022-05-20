using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Entities
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Location { get; set; }

        public DateTime CreatedAt => DateTime.Now;
        public DateTime UpdatedAt => DateTime.Now;

        public ICollection<Product>? Products { get; set; }

    }
}
