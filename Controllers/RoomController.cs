using HotelMangementSystem.Models;
using HotelMangementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelMangementSystem.Controllers
{
    public class RoomController:Controller
    {
        private readonly IRoomRepo roomRepo;

        public RoomController(IRoomRepo roomRepo)
        {
            this.roomRepo = roomRepo;
        }

        #region Index
        public IActionResult Index()
        {
            List<Room> rooms = roomRepo.GetAll();
            return View("Index", rooms);
        }
        #endregion

        #region Details
        public IActionResult Details(int id)
        {
            Room room = roomRepo.GetById(id);
            if (room != null)
            {
                return View("Details", room);
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
        public IActionResult Create(Room room)
        {
            if (ModelState.IsValid)
            {
                roomRepo.Insert(room);
                roomRepo.Save();
                return RedirectToAction("Index");
            }
            return View("Create", room);
        }
        #endregion

        #region Edit
        public IActionResult Edit(int id)
        {
            Room room = roomRepo.GetById(id);
            if (room != null)
            {
                return View("Edit", room);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Room room)
        {
            if (ModelState.IsValid)
            {
                roomRepo.Update(room);
                roomRepo.Save();
                return RedirectToAction("Index");
            }
            return View("Edit", room);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            Room room = roomRepo.GetById(id);
            if (room != null)
            {
                return View("Delete", room);
            }
            return NotFound();
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            roomRepo.Delete(id);
            roomRepo.Save();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
