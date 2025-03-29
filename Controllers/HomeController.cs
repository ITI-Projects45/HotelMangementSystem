using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HotelMangementSystem.Models;
using HotelMangementSystem.Repositories;
using HotelMangementSystem.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace HotelMangementSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHotelRepo hotelRepo;
    private readonly IReviewRepo reviewRepo;
    private readonly IPendingHotelRepo pendingHotelRepo;
    private readonly IHubContext<HAddHotelHub> addHotelHub;

    public HomeController(ILogger<HomeController> logger, IHotelRepo hotelRepo, IReviewRepo reviewRepo, IPendingHotelRepo pendingHotelRepo, IHubContext<HAddHotelHub> addHotelHub)
    {
        _logger = logger;
        this.hotelRepo = hotelRepo;
        this.reviewRepo = reviewRepo;
        this.pendingHotelRepo = pendingHotelRepo;
        this.addHotelHub = addHotelHub;
    }

    public IActionResult Index()
    {
        List<Hotel> hotels = hotelRepo.GetAll();
        ViewBag.hotels = hotels;
        List<Review> reviews = reviewRepo.getAllWithUserAndHotels();
        ViewBag.reviews = reviews;
        ViewBag.PendingHotelNumberNotification = pendingHotelRepo.GetPendingHotels().Count();

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
