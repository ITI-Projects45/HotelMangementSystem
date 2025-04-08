using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using HotelMangementSystem.Models;
using HotelMangementSystem.Repositories;
using HotelMangementSystem.Hubs;
using HotelMangementSystem.ViewModels;
using System.Security.Claims;

namespace HotelMangementSystem.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepo reviewRepo;
        private readonly IHotelRepo hotelRepo; 
        private readonly IHubContext<ReviewHub> hubContext;
        public ReviewController(IReviewRepo reviewRepo , IHotelRepo hotelRepo , IHubContext<ReviewHub> hubContext)
        {
            this.reviewRepo = reviewRepo;
            this.hotelRepo = hotelRepo;
            this.hubContext = hubContext;
        }

        public async Task<IActionResult> AddReview(int hotelId) {

            Hotel hotel = await hotelRepo.GetHotelByIdAsync(hotelId);
            ReviewViewModel reviewViewModel = new ReviewViewModel { hotelId = hotelId };
            return View("AddReview", reviewViewModel);
        }
        public async Task<IActionResult> AddReview(ReviewViewModel reviewViewModel)
        {
            if (!ModelState.IsValid) { return View("AddReview", reviewViewModel); }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var review = new Review
            {
                HotelId = reviewViewModel.hotelId,
                StarNumber = reviewViewModel.StarNumber,
                Content = reviewViewModel.Content,
                ReviewDate = DateTime.Now,
                IsDeleted = false,
                UserReview = new UserReview
                {
                    UserId = userId
                }
            };

             reviewRepo.Insert(review);

            await hubContext.Clients.All.SendAsync("ReceiveReview", new
            {
                review.Content,
                review.StarNumber,
                ReviewDate = review.ReviewDate.ToString("g"),
                HotelId = review.HotelId
            });
            return RedirectToAction("Hotel", "Hotels", new { id = reviewViewModel.hotelId });


            return Ok();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
