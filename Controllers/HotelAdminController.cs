using HotelMangementSystem.Hubs;
using HotelMangementSystem.Models;
using HotelMangementSystem.Repositories;
using HotelMangementSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HotelMangementSystem.Controllers
{
    public class HotelAdminController : Controller
    {
        private readonly ICityRepo cityRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHotelRepo hotelRepo;
        private readonly IHubContext<HAddHotelHub> addHotelHub;
        private readonly IPendingHotelRepo pendingHotelRepo;

        public HotelAdminController(ICityRepo cityRepo, UserManager<ApplicationUser> userManager, IHotelRepo hotelRepo, IHubContext<HAddHotelHub> addHotelHub, IPendingHotelRepo pendingHotelRepo)
        {
            this.cityRepo = cityRepo;
            this.userManager = userManager;
            this.hotelRepo = hotelRepo;
            this.addHotelHub = addHotelHub;
            this.pendingHotelRepo = pendingHotelRepo;
        }

        [HttpGet]
        public IActionResult AddNewHotel()
        {
            string userId = userManager.GetUserId(User);
            PendingHotel HotelFromDb = pendingHotelRepo.GetRequestByManagerID(userId);

            if (userId == null)
            {
                ViewBag.UserId = "Invalid User";
            }
            else
            {
                ViewBag.UserId = userId;

                if (HotelFromDb != null)
                {
                    ViewBag.oldHotelFromDb = HotelFromDb;

                }

            }

            if (HotelFromDb != null)
            {

                ViewBag.Cities = new
                {
                    cities = cityRepo.GetAll(),
                    selectedCity = HotelFromDb.CityId
                };
            }

            ViewBag.Cities = new
            {
                cities = cityRepo.GetAll(),
                selectedCity = -1
            };


            return View();
        }



        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddNewHotel(NewHotelViewModel newHotelFromRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (newHotelFromRequest.CityId != -1)
                    {
                        ApplicationUser manager = await userManager.FindByIdAsync(newHotelFromRequest.ManagerId);
                        City city = cityRepo.GetById(newHotelFromRequest.CityId);

                        PendingHotel hotel = new PendingHotel()
                        {
                            Name = newHotelFromRequest.Name,
                            Description = newHotelFromRequest.Description,
                            StarRatig = newHotelFromRequest.StarRatig,
                            Location = newHotelFromRequest.Location,
                            PhoneNumber = newHotelFromRequest.PhoneNumber,
                            NumberOfRooms = newHotelFromRequest.NumberOfRooms,
                            ManagerId = newHotelFromRequest.ManagerId,
                            CityId = newHotelFromRequest.CityId,


                        };

                        PendingHotel ExistingHotel = pendingHotelRepo.CheckIfExists(newHotelFromRequest.Name, newHotelFromRequest.ManagerId);
                        if (ExistingHotel == null)
                        {

                            pendingHotelRepo.Insert(hotel);
                            ViewBag.HotelAdded = true;
                            pendingHotelRepo.Save();
                        }
                        else
                        {
                            ViewBag.AlreadyAdded = true;
                        }



                    }
                    else
                    {
                        ViewBag.HotelAdded = false;

                        ModelState.AddModelError("", "Please Choose Valid Cit");
                    }
                }
                catch (Exception e)
                {
                    ViewBag.HotelAdded = false;

                    ModelState.AddModelError("", e.Message);
                }


            }

            string userId = userManager.GetUserId(User);

            ViewBag.UserId = userId;

            ViewBag.Cities = new
            {
                cities = cityRepo.GetAll(),
                selectedCity = -1
            };

            return View(newHotelFromRequest);
        }
    }
}
