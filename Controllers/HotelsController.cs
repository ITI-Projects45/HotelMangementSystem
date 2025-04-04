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
        [HttpGet]
        public async Task<IActionResult> Index(string cityName, string sortOrder = "desc", int page = 1, int pageSize = 6)
        {
            var hotels = await hotelRepo.GetHotelsByCityOrderdByStartsAsync(cityName, page, pageSize, sortOrder);

            var hotelViewModels = hotels.Select(h => new HotelViewModel
            {
                Id = h.Id,
                Name = h.Name,
                Location = h.City.Name,
                PhoneNumber = h.PhoneNumber,
                Description = h.Description,
                NumberOfRooms = h.NumberOfRooms,
                StarRating = h.StarRatig
            }).ToList();



            int totalHotels = await hotelRepo.GetTotalHotelsCountAsync(cityName);
            ViewBag.Cities = await hotelRepo.GetAllCitiesAsync();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalHotels / pageSize);
            ViewBag.CityName = cityName;
            ViewBag.SortOrder = sortOrder;

            return View(hotelViewModels);
        }

        public async Task<IActionResult> Hotel(int id)
        {
            Hotel hotel = await hotelRepo.GetHotelByIdAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View("Hotel", hotel);
        }

    }
}
