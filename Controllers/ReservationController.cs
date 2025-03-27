using HotelMangementSystem.Models;
using HotelMangementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelMangementSystem.Controllers
{
    public class ReservationController:Controller
    {
        private readonly IReservationRepo reservationRepo;

        public ReservationController(IReservationRepo reservationRepo)
        {
            this.reservationRepo = reservationRepo;
        }

        #region Index
        public IActionResult Index()
        {
            List<Reservation> reservations = reservationRepo.GetAll();
            return View("Index", reservations);
        }
        #endregion

        #region Details
        public IActionResult Details(int id)
        {
            Reservation reservation = reservationRepo.GetById(id);
            if (reservation != null)
            {
                return View("Details", reservation);
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
        public IActionResult Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                reservationRepo.Insert(reservation);
                reservationRepo.Save();
                return RedirectToAction("Index");
            }
            return View("Create", reservation);
        }
        #endregion

        #region Edit
        public IActionResult Edit(int id)
        {
            Reservation reservation = reservationRepo.GetById(id);
            if (reservation != null)
            {
                return View("Edit", reservation);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                reservationRepo.Update(reservation);
                reservationRepo.Save();
                return RedirectToAction("Index");
            }
            return View("Edit", reservation);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            Reservation reservation = reservationRepo.GetById(id);
            if (reservation != null)
            {
                return View("Delete", reservation);
            }
            return NotFound();
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            reservationRepo.Delete(id);
            reservationRepo.Save();
            return RedirectToAction("Index");
        }
        #endregion

    }
}
