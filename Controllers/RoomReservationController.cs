using HotelMangementSystem.Models;
using HotelMangementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelMangementSystem.Controllers
{
    public class RoomReservationController:Controller
    {
        private readonly IRoomReservationRepo roomReservationRepo;

        public RoomReservationController(IRoomReservationRepo roomReservationRepo)
        {
            this.roomReservationRepo = roomReservationRepo;
        }

        #region Index
        public IActionResult Index()
        {
            List<RoomReservation> roomReservations = roomReservationRepo.GetAll();
            return View("Index", roomReservations);
        }
        #endregion

        #region Details
        public IActionResult Details(int id)
        {
            RoomReservation roomReservation = roomReservationRepo.GetById(id);
            if (roomReservation != null)
            {
                return View("Details", roomReservation);
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
        public IActionResult Create(RoomReservation roomReservation)
        {
            if (ModelState.IsValid)
            {
                roomReservationRepo.Insert(roomReservation);
                roomReservationRepo.Save();
                return RedirectToAction("Index");
            }
            return View("Create", roomReservation);
        }
        #endregion

        #region Edit
        public IActionResult Edit(int id)
        {
            RoomReservation roomReservation = roomReservationRepo.GetById(id);
            if (roomReservation != null)
            {
                return View("Edit", roomReservation);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(RoomReservation roomReservation)
        {
            if (ModelState.IsValid)
            {
                roomReservationRepo.Update(roomReservation);
                roomReservationRepo.Save();
                return RedirectToAction("Index");
            }
            return View("Edit", roomReservation);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            RoomReservation roomReservation = roomReservationRepo.GetById(id);
            if (roomReservation != null)
            {
                return View("Delete", roomReservation);
            }
            return NotFound();
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            roomReservationRepo.Delete(id);
            roomReservationRepo.Save();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
