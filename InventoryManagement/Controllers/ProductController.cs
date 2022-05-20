using AutoMapper;
using InventoryManagement.Entities;
using InventoryManagement.Interfaces;
using InventoryManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        private readonly IInventoryRepo _inventoryRepo;

        public ProductController(IProductRepo productrepo, IMapper mapper, IInventoryRepo inventoryRepo)
        {
            _productRepo = productrepo;
            _mapper = mapper;
            _inventoryRepo = inventoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct(int id)
        {
            var model = await _inventoryRepo.Find(id);
           
            var Model = new AddProductViewModel();
            Model.InventoryId = model.Id;
            return View( "AddProduct", Model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //mapping the models
                    var newproduct = _mapper.Map<Product>(model);
                    
                    newproduct.InventoryId = id;

                    await _productRepo.Save(newproduct);
                    var returnedModel = await _productRepo.FindAllProductswithinventory(id);
                    return View("ManageProduct", returnedModel);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }
            }
            var returnModel =  await _productRepo.FindAllProductswithinventory(id);
            return View("ManageProduct", returnModel);
        }


        //[HttpGet]
        //public async Task<IActionResult> Details(int )
        //{
        //    var model = await _productRepo.FindInventoryWithProduct(id);
        //    return View("Details", model);
        //}

        [HttpGet]
        public async Task<IActionResult> ManageProduct(int id)
        {
            var returnModel = await _productRepo.FindAllProductswithinventory(id);
            return View("ManageProduct", returnModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDetails(int id)
        {
            var returnModel = await _productRepo.FindProductbyId(id);
            return View("DeleteProduct",  returnModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditDetails(int id)
        {
            var returnModel = await _productRepo.FindProductbyId(id);
            var editModel = _mapper.Map<EditProductViewModel>(returnModel);
            return View("EditProduct", editModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(Product Model,int id)
        {
            try
            {
                var product = await _productRepo.Find(id);
                if (product != null)
                {
                    product.IsDeleted = true;
                    product.DeleteComments = Model.DeleteComments;
                    await _productRepo.Update(product);
                }
                var model = await _productRepo.FindAllProductswithinventory(product.InventoryId);
                return View("ManageProduct", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            var returnModel = await _productRepo.FindAllProductswithinventory(id);
            return View("ManageProduct", returnModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetdeletedProducts(int id)
        {
            var deletedproducts = await _productRepo.FindDeletedProductsById(id);
            return View("ManageDeletedProducts", deletedproducts);
        }

        [HttpGet]
        public async Task<IActionResult> GetdeletedProduct(int id)
        {
            var deletedproduct = await _productRepo.FindDeletedProductById(id);
            return View("RestoreDelete", deletedproduct);
        }

        [HttpPost]
        public async Task<IActionResult> RestoreDelete(Product Model, int id)
        {
            try
            {
                var product = await _productRepo.Find(id);
                if (product != null)
                {
                    product.IsDeleted = false;
                    product.DeleteComments = Model.DeleteComments;
                    await _productRepo.Update(product);
                }
                var reModel = await _productRepo.FindAllProductswithinventory(product.InventoryId);
                return View("ManageProduct", reModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            var returnModel = await _productRepo.FindAllProductswithinventory(id);
            return View("ManageProduct", returnModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = await _productRepo.Find(model.Id);
                    product.Name = model.Name;
                    product.Price = model.Price;
                    product.Details = model.Details;
                    product.Quantity = model.Quantity;
                    
                    await _productRepo.Update(product);
                    var returnedModel = await _productRepo.FindAllProductswithinventory(product.InventoryId);
                    return View("ManageProduct", returnedModel);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }
            }
            return View("Index");
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View("CreateInventory");
        }
    }
}
