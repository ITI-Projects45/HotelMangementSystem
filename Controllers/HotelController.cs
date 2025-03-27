using HotelMangementSystem.Models;
using HotelMangementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace HotelMangementSystem.Controllers
{
    public class HotelController:Controller
    {
        private readonly IHotelRepo hotelRepo;

        public HotelController(IHotelRepo hotelRepo)
        {
            this.hotelRepo = hotelRepo;
        }

        #region Index
        public IActionResult Index()
        {
            List<Hotel> hotels = hotelRepo.GetAll();
            return View("Index", hotels);
        }
        #endregion

        #region Details
        public IActionResult Details(int id)
        {
            Hotel hotel = hotelRepo.GetById(id);
            if (hotel != null)
            {
                return View("Details", hotel);
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
        public IActionResult Create(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                hotelRepo.Insert(hotel);
                hotelRepo.Save();
                return RedirectToAction("Index");
            }
            return View("Create", hotel);
        }
        #endregion

        #region Edit
        public IActionResult Edit(int id)
        {
            Hotel hotel = hotelRepo.GetById(id);
            if (hotel != null)
            {
                return View("Edit", hotel);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                hotelRepo.Update(hotel);
                hotelRepo.Save();
                return RedirectToAction("Index");
            }
            return View("Edit", hotel);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            Hotel hotel = hotelRepo.GetById(id);
            if (hotel != null)
            {
                return View("Delete", hotel);
            }
            return NotFound();
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            hotelRepo.Delete(id);
            hotelRepo.Save();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
