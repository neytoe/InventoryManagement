using AutoMapper;
using InventoryManagement.Entities;
using InventoryManagement.ViewModels;

namespace InventoryManagement.Maps
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddInventoryViewModel, Inventory>();
            CreateMap<AddProductViewModel, Product>();
            CreateMap<EditProductViewModel, Product>();
            CreateMap<Product, EditProductViewModel>();
        }
    }
}
