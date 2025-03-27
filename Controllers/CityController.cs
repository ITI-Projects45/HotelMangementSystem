using HotelMangementSystem.Models;
using HotelMangementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelMangementSystem.Controllers
{
    public class CityController:Controller
    {
        private readonly ICityRepo cityRepo;

        public CityController(ICityRepo cityRepo)
        {
            this.cityRepo = cityRepo;
        }

        #region Index
        public IActionResult Index()
        {
            List<City> cities = cityRepo.GetAll();
            return View("Index", cities);
        }
        #endregion

        #region Details
        public IActionResult Details(int id)
        {
            City city = cityRepo.GetById(id);
            if (city != null)
            {
                return View("Details", city);
            }
            return NotFound();
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(City city)
        {
            if (ModelState.IsValid)
            {
                cityRepo.Insert(city);
                cityRepo.Save();
                return RedirectToAction("Index");
            }
            return View("Create", city);
        }
        #endregion

        #region Edit
        public IActionResult Edit(int id)
        {
            City city = cityRepo.GetById(id);
            if (city != null)
            {
                return View("Edit", city);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(City city)
        {
            if (ModelState.IsValid)
            {
                cityRepo.Update(city);
                cityRepo.Save();
                return RedirectToAction("Index");
            }
            return View("Edit", city);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            City city = cityRepo.GetById(id);
            if (city != null)
            {
                return View("Delete", city);
            }
            return NotFound();
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            cityRepo.Delete(id);
            cityRepo.Save();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
