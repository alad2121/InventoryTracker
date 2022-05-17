using InventoryTracker.Data;
using InventoryTracker.Models;
using InventoryTracker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.Controllers
{
    public class ProductController : Controller
    {

        private readonly ApplicationDbContext _db;

        private int _productId = 1;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductModel> products = _db.Products;

            return View(products);
        }

        [HttpGet]

        public IActionResult Create()
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _db.Categories.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                LocationList = _db.Locations.Select(u => new SelectListItem
                {
                    Text = u.WarehouseName,
                    Value = u.Id.ToString()
                })
            };



            return View(productVM);
        }


        [HttpPost]
        public IActionResult Create(ProductVM obj)
        {

            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _db.Categories.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                LocationList = _db.Locations.Select(u => new SelectListItem
                {
                    Text = u.WarehouseName,
                    Value = u.Id.ToString()
                })
            };

            //if (ModelState.IsValid)
            //{

            for (int i = 0; i < _db.Products.ToArray().Length; i++)
            {
                _productId++;
            }

            obj.Product.Id = _productId;

            obj.Product.selectedCategory = productVM.CategoryList.ElementAt<SelectListItem>(obj.Product.CategoryId - 1).Text;
            obj.Product.selectedLocation = productVM.LocationList.ElementAt<SelectListItem>(obj.Product.LocationId - 1).Text;
            //obj.Product.selectedWareHouse = productVM.LocationList.ElementAt<SelectListItem>(obj.Product.CategoryId - 1).Text;


            _db.Products.Add(obj.Product);

            _db.SaveChanges();
            return RedirectToAction("Index");

            //}
            //return View("Create");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var product = _db.Products.FirstOrDefault(x => x.Id == id);


            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(ProductModel obj)
        {

            List<ProductModel> products = _db.Products.AsNoTracking().ToList();

            int objId = obj.Id;


            int amountOfProducts = products.Count();
            if (amountOfProducts > 1 && obj.Id != amountOfProducts)
            {
                _db.Products.Remove(obj);

                foreach (ProductModel product in products)
                {
                    if (product.Id > objId)
                    {
                        _db.Products.Add(new ProductModel { Id = product.Id - 1, Name = product.Name});
                        _db.Products.Remove(product);
                        _db.SaveChanges();
                    }
                }

            }
            else
            {
                _db.Products.Remove(obj);

                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]

        public IActionResult Edit(int? id)
        {


            ProductVM selectedProduct = new()
            {
                Product = new(),
                CategoryList = _db.Categories.Select(id => new SelectListItem
                {
                    Value = id.Name,
                    Text = id.Name
                }),
                LocationList = _db.Locations.Select(id => new SelectListItem
                {
                    Value = id.Country,
                    Text = id.Country
                })
            };
            if (id == null || id == 0)
            {
                return NotFound();
            }


            selectedProduct.Product = _db.Products.FirstOrDefault(u => u.Id == id);


            return View(selectedProduct);
        }

        [HttpPost]
        public IActionResult Edit(ProductVM obj)
        {
            var selectedProduct = _db.Products.FirstOrDefault(u => u.Id == obj.Product.Id);

            selectedProduct.Name = obj.Product.Name;
            selectedProduct.Location = obj.Product.Location;
            selectedProduct.Price = obj.Product.Price;
            selectedProduct.Category = obj.Product.Category;

            _db.Products.Update(selectedProduct);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
