using HotelMangementSystem.Models;
using HotelMangementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelMangementSystem.Controllers
{
    public class ReviewController :Controller
    {
        private readonly IReviewRepo reviewRepo;

        public ReviewController(IReviewRepo reviewRepo)
        {
            this.reviewRepo = reviewRepo;
        }

        #region Index
        public IActionResult Index()
        {
            List<Review> reviews = reviewRepo.GetAll();
            return View("Index", reviews);
        }
        #endregion

        #region Details
        public IActionResult Details(int id)
        {
            Review review = reviewRepo.GetById(id);
            if (review != null)
            {
                return View("Details", review);
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
        public IActionResult Create(Review review)
        {
            if (ModelState.IsValid)
            {
                reviewRepo.Insert(review);
                reviewRepo.Save();
                return RedirectToAction("Index");
            }
            return View("Create", review);
        }
        #endregion

        #region Edit
        public IActionResult Edit(int id)
        {
            Review review = reviewRepo.GetById(id);
            if (review != null)
            {
                return View("Edit", review);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Review review)
        {
            if (ModelState.IsValid)
            {
                reviewRepo.Update(review);
                reviewRepo.Save();
                return RedirectToAction("Index");
            }
            return View("Edit", review);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            Review review = reviewRepo.GetById(id);
            if (review != null)
            {
                return View("Delete", review);
            }
            return NotFound();
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            reviewRepo.Delete(id);
            reviewRepo.Save();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
