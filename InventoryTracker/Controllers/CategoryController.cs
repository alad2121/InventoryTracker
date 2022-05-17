using InventoryTracker.Data;
using InventoryTracker.Models;
using InventoryTracker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.Controllers
{
    public class CategoryController : Controller
    {


        private int _categoryId = 1;
        public readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            CategoryVM categoryVM = new()
            {
                Categories = _db.Categories,
                Products = _db.Products,
            };
            foreach (CategoryModel category in categoryVM.Categories)
            {
                int numberOfProducts = categoryVM.Products.Where(u => u.selectedCategory == category.Name).Count();

                category.NumberOfProducts = numberOfProducts;
            }

            return View(categoryVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }



        [HttpPost]
        public IActionResult Create(CategoryModel obj)
        {
            {

                if (ModelState.IsValid)
                {

                    try
                    {
                        for (int i = 0; i < _db.Categories.ToArray().Length; i++)
                        {
                            _categoryId++;
                        }
                    }
                    catch (InvalidOperationException err)
                    {
                        _categoryId++;
                    }



                    obj.Id = _categoryId;

                    _db.Categories.Add(obj);
                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View("Create");
            }

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {


            if (id == null || id == 0)
            {
                return NotFound();
            }

            var selectedCategory = _db.Categories.FirstOrDefault(c => c.Id == id);


            if (selectedCategory == null)
            {
                return NotFound();
            }

            return View(selectedCategory);

        }

        [HttpPost]
        public IActionResult Delete(CategoryModel obj)
        {

            List<CategoryModel> catergories = _db.Categories.AsNoTracking().ToList();

            int objId = obj.Id;


            int amountCat = catergories.Count();
            if (amountCat > 1 && obj.Id != amountCat)
            {
                _db.Categories.Remove(obj);

                foreach (CategoryModel category in catergories)
                {
                    if (category.Id > objId)
                    {
                        _db.Categories.Add(new CategoryModel { Id = category.Id - 1, Name = category.Name, NumberOfProducts = category.NumberOfProducts });
                        _db.Categories.Remove(category);
                        _db.SaveChanges();
                    }
                }
               
            }
            else
            {
                _db.Categories.Remove(obj);

                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

      

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var selectedCategory = _db.Categories.FirstOrDefault(x => x.Id == id);

            if (selectedCategory == null)
            {
                return NotFound();
            }

            return View(selectedCategory);


        }

        [HttpPost]
        public IActionResult Edit(CategoryModel obj)
        {
            var selectedCategory = _db.Categories.FirstOrDefault(x => x.Id == obj.Id);

            selectedCategory.Name = obj.Name;

            _db.Categories.Update(selectedCategory);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}

