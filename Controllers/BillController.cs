using HotelMangementSystem.Models;
using HotelMangementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace HotelMangementSystem.Controllers
{
    public class BillController:Controller
    {
        private readonly IBillRepo billRepo;

        public BillController(IBillRepo billRepo)
        {
            this.billRepo = billRepo;
        }
        #region Index
        public IActionResult Index() {
            List<Bill> bills = billRepo.GetAll();
            return View("Index",bills);
        }
        #endregion
        #region Details
        public IActionResult Details(int id) {
            Bill bill = billRepo.GetById(id);
            if (bill != null) {
                return View("Details", bill);
            }
            return NotFound();
        }
        #endregion
        #region Create
        public IActionResult Create() {
            return View();
        }
        public IActionResult Create(Bill bill) {
            if (ModelState.IsValid) {
                billRepo.Insert(bill);
                billRepo.Save();
                return RedirectToAction("Index");

            }
            return View("Create", bill);
        }

        #endregion


        #region Edit
        public IActionResult Edit(int id) {

            Bill bill = billRepo.GetById(id);
            if (bill != null) {
            return View("Edit", bill);
            }
            return NotFound();
        
        }
        public IActionResult Edit(Bill bill) {
            if (ModelState.IsValid) {
                billRepo.Update(bill);
                billRepo.Save();
                return RedirectToAction("Index");
            }
            return View("Edit",bill);
        }

        #endregion

        #region Delete
        public IActionResult Delete(int id) {
            Bill bill = billRepo.GetById(id);
            if (bill != null) {
                return View("Delete", bill);
            }
            return NotFound();
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id) {
            billRepo.Delete(id);
            billRepo.Save();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
