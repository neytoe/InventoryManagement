using AutoMapper;
using InventoryManagement.Entities;
using InventoryManagement.Interfaces;
using InventoryManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        private readonly IInventoryRepo _inventoryRepo;

        public InventoryController(IProductRepo productrepo, IMapper mapper, IInventoryRepo inventoryRepo)
        {
            _productRepo = productrepo;
            _mapper = mapper;
            _inventoryRepo = inventoryRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("CreateInventory");
        }

        [HttpGet]
        public async Task<IActionResult> ViewInventory()
        {
            var inventory = await _inventoryRepo.FindAll();
            return View("ManageInventory", inventory);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AddInventoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //mapping the models
                    var newinventory = _mapper.Map<Inventory>(model);

                    await _inventoryRepo.Save(newinventory);
                    var inventorylist = await _inventoryRepo.FindAll();
                    return View("ManageInventory",inventorylist);
                    // await _productRepo.Save(newproduct);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }
            }
            var list = await _inventoryRepo.FindAll();
            return View("ManageInventory", list);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var returnModel = await _inventoryRepo.Find(id);
            return View("ManageProduct", returnModel);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            await _inventoryRepo.Delete(id);
            var inventory = await _inventoryRepo.FindAll();
            return View("ManageInventory", inventory);
            
        }


     
    }
}
