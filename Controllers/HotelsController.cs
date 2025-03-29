﻿using HotelMangementSystem.Models;
using HotelMangementSystem.Repositories;
using HotelMangementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelMangementSystem.Controllers
{
    public class HotelsController : Controller
    {
        private readonly IHotelRepo hotelRepo;

        public HotelsController(IHotelRepo hotelRepo)
        {
            this.hotelRepo = hotelRepo;
        }

        public async Task<IActionResult> Index(string cityName, int page = 1, int pageSize = 5)
        {
            var hotels = await hotelRepo.GetHotelsByCityAsync(cityName, page, pageSize);

            var hotelViewModels = hotels.Select(h => new HotelViewModel
            {
                Id = h.Id,
                Name = h.Name,
                Location = h.City.Name,
                PhoneNumber = h.PhoneNumber,
                Description = h.Description
            }).ToList();

            int totalHotels = await hotelRepo.GetTotalHotelsCountAsync(cityName);
            ViewBag.Cities = await hotelRepo.GetAllCitiesAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalHotels / pageSize);
            ViewBag.CityName = cityName;

            return View(hotelViewModels);
        }

    }
}
