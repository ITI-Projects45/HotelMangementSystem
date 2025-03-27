using HotelMangementSystem.Models;
using HotelMangementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelMangementSystem.Controllers
{
    public class UserReviewController :Controller
    {
        private readonly IUserReviewRepo userReviewRepo;

        public UserReviewController(IUserReviewRepo userReviewRepo)
        {
            this.userReviewRepo = userReviewRepo;
        }

        #region Index
        public IActionResult Index()
        {
            List<UserReview> userReviews = userReviewRepo.GetAll();
            return View("Index", userReviews);
        }
        #endregion

        #region Details
        public IActionResult Details(int id)
        {
            UserReview userReview = userReviewRepo.GetById(id);
            if (userReview != null)
            {
                return View("Details", userReview);
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
        public IActionResult Create(UserReview userReview)
        {
            if (ModelState.IsValid)
            {
                userReviewRepo.Insert(userReview);
                userReviewRepo.Save();
                return RedirectToAction("Index");
            }
            return View("Create", userReview);
        }
        #endregion

        #region Edit
        public IActionResult Edit(int id)
        {
            UserReview userReview = userReviewRepo.GetById(id);
            if (userReview != null)
            {
                return View("Edit", userReview);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(UserReview userReview)
        {
            if (ModelState.IsValid)
            {
                userReviewRepo.Update(userReview);
                userReviewRepo.Save();
                return RedirectToAction("Index");
            }
            return View("Edit", userReview);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            UserReview userReview = userReviewRepo.GetById(id);
            if (userReview != null)
            {
                return View("Delete", userReview);
            }
            return NotFound();
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            userReviewRepo.Delete(id);
            userReviewRepo.Save();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
