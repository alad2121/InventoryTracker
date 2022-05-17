using InventoryTracker.Data;
using InventoryTracker.Models;
using InventoryTracker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.Controllers
{
    public class LocationController : Controller
    {

        private int _locationId = 1;


        public readonly ApplicationDbContext _db;

        public LocationController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            LocationVM locationVm = new()
            {
                Locations = _db.Locations,
                Products = _db.Products,
            };


            foreach (LocationModel location in locationVm.Locations)
            {
                location.NumberOfProducts = locationVm.Products.Where(u => u.selectedLocation.Equals(location.WarehouseName)).Count();

            }


            return View(locationVm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }


        [HttpPost]
        public IActionResult Create(LocationModel obj)
        {
            {


                for (int i = 0; i < _db.Products.ToArray().Length; i++)
                {
                    _locationId++;
                }

                obj.Id = _locationId;
                _db.Locations.Add(obj);
                _db.SaveChanges();

                return RedirectToAction("Index");


                //return View("Create");
            }

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var selectedLocation = _db.Locations.FirstOrDefault(x => x.Id == id);

            if (selectedLocation == null)
            {
                return NotFound();
            }

            return View(selectedLocation);

        }
        [HttpPost]
        public IActionResult Delete(LocationModel obj)
        {


            List<LocationModel> locations = _db.Locations.AsNoTracking().ToList();

            int objId = obj.Id;


            int amountOfLocations = locations.Count();
            if (amountOfLocations > 1 && obj.Id != amountOfLocations)
            {
                _db.Locations.Remove(obj);

                foreach (LocationModel location in locations)
                {
                    if (location.Id > objId)
                    {
                        _db.Locations.Add(new LocationModel { Id = location.Id - 1, State = location.State, City = location.City, Country = location.Country, NumberOfProducts = location.NumberOfProducts, WarehouseName = location.WarehouseName });
                        _db.Locations.Remove(location);
                        _db.SaveChanges();
                    }
                }

            }
            else
            {
                _db.Locations.Remove(obj);

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
            var selectedLocation = _db.Locations.FirstOrDefault(x => x.Id == id);

            if (selectedLocation == null)
            {
                return NotFound();
            }

            return View(selectedLocation);


        }

        [HttpPost]
        public IActionResult Edit(LocationModel obj)
        {
            var selectedLocation = _db.Locations.FirstOrDefault(x => x.Id == obj.Id);
            selectedLocation.State = obj.State;
            selectedLocation.City = obj.City;
            selectedLocation.WarehouseName = obj.WarehouseName;
            selectedLocation.Country = obj.Country;


            _db.Locations.Update(selectedLocation);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}

