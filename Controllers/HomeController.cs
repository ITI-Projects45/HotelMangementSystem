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
    private readonly ICityRepo cityRepo;

    public HomeController(ILogger<HomeController> logger, IHotelRepo hotelRepo, IReviewRepo reviewRepo, IPendingHotelRepo pendingHotelRepo, ICityRepo cityRepo, IHubContext<HAddHotelHub> addHotelHub)
    {
        _logger = logger;
        this.hotelRepo = hotelRepo;
        this.reviewRepo = reviewRepo;
        this.pendingHotelRepo = pendingHotelRepo;
        this.addHotelHub = addHotelHub;
        this.cityRepo = cityRepo;
    }

    public IActionResult Index()
    {
        List<Hotel> hotels = hotelRepo.GetFourTopRatedRandomizedHotels();
        ViewBag.hotels = hotels;
        List<Review> reviews = reviewRepo.getAllWithUserAndHotels();
        ViewBag.reviews = reviews;
        List<City> cities = cityRepo.GetCities();
        ViewBag.cities = cities;

        ViewBag.PendingHotelNumberNotification = pendingHotelRepo.GetPendingHotels().Count();

        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
