using InventoryManagement.Entities;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.ViewModels
{
    public class EditProductViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Details { get; set; }

        public int InventoryId { get; set; }    
    }
}
