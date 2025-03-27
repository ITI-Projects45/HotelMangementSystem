using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HotelMangementSystem.Models;
using HotelMangementSystem.Repositories;

namespace HotelMangementSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHotelRepo hotelRepo;
    private readonly IReviewRepo reviewRepo;

    public HomeController(ILogger<HomeController> logger, IHotelRepo hotelRepo, IReviewRepo reviewRepo)
    {
        _logger = logger;
        this.hotelRepo = hotelRepo;
        this.reviewRepo = reviewRepo;
    }

    public IActionResult Index()
    {
        List<Hotel> hotels = hotelRepo.GetAll();
        ViewBag.hotels = hotels;
        List<Review> reviews = reviewRepo.getAllWithUserAndHotels();
        ViewBag.reviews = reviews;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
