using HotelMangementSystem.Hubs;
using HotelMangementSystem.Models;
using HotelMangementSystem.Repositories;
using HotelMangementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using static HotelMangementSystem.Models.Enums.Enums;

namespace HotelMangementSystem.Controllers
{
    public class HotelAdminController : Controller
    {
        private readonly ICityRepo cityRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHotelRepo hotelRepo;
        private readonly IHubContext<HAddHotelHub> addHotelHub;
        private readonly IPendingHotelRepo pendingHotelRepo;
        private readonly RoleManager<ApplicationRole> roleManager;

        public HotelAdminController(ICityRepo cityRepo, UserManager<ApplicationUser> userManager, IHotelRepo hotelRepo, IHubContext<HAddHotelHub> addHotelHub, IPendingHotelRepo pendingHotelRepo, RoleManager<ApplicationRole> roleManager)
        {
            this.cityRepo = cityRepo;
            this.userManager = userManager;
            this.hotelRepo = hotelRepo;
            this.addHotelHub = addHotelHub;
            this.pendingHotelRepo = pendingHotelRepo;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult AddNewHotel()
        {
            string userId = userManager.GetUserId(User);
            PendingHotel HotelFromDb = pendingHotelRepo.GetRequestByManagerID(userId);
            List<City> citiesList = cityRepo.GetCities();

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
                    ViewBag.requestStatus = NewHotelRquestStatus.Pending;

                }

            }

            if (HotelFromDb != null)
            {

                ViewBag.Cities = new
                {
                    cities = citiesList,
                    selectedCity = HotelFromDb.CityId
                };
            }
            else
            {
                ViewBag.Cities = new
                {
                    cities = citiesList,
                    selectedCity = -1
                };
            }
            //ViewBag.Cities = cities;

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
                            int count = pendingHotelRepo.GetPendingHotels().Count();
                            await addHotelHub.Clients.All.SendAsync("NewHotelAdded", count);
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
                cities = cityRepo.GetCities(),
                selectedCity = -1
            };

            return View(newHotelFromRequest);
        }



        public IActionResult PendingHotels()
        {

            List<PendingHotel> hotels = pendingHotelRepo.GetPendingHotels();

            return View(hotels);
        }

        public IActionResult PendingHotel(int id)
        {
            PendingHotel hotel = pendingHotelRepo.GetById(id);
            City c = cityRepo.GetById(hotel.CityId);
            ViewBag.CityName = c.Name;
            if (hotel != null)
            {
                return View("PendingHotel", hotel);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PendingHotelAsync(PendingHotel newHotelFromReq)
        {
            Hotel newHotel = new Hotel()
            {
                Name = newHotelFromReq.Name,
                City = newHotelFromReq.City,
                CityId = newHotelFromReq.CityId,
                Description = newHotelFromReq.Description,
                Location = newHotelFromReq.Location,
                Manager = newHotelFromReq.Manager,
                StarRatig = newHotelFromReq.StarRatig,
                NumberOfRooms = newHotelFromReq.NumberOfRooms,
                PhoneNumber = newHotelFromReq.PhoneNumber,
                ManagerId = newHotelFromReq.ManagerId,
                HotelStatus = NewHotelRquestStatus.Accepted


            };


            ApplicationUser user = await userManager.FindByIdAsync(newHotelFromReq.ManagerId);
            hotelRepo.Insert(newHotel);
            hotelRepo.Save();
            newHotelFromReq.IsDeleted = true;

            if (!User.IsInRole("HotelAdmin"))
            {
                await userManager.AddToRoleAsync(user, "HotelAdmin");
            }

            pendingHotelRepo.Update(newHotelFromReq);
            pendingHotelRepo.Save();

            string UserConnection = HAddHotelHub.ClientsId[$"{user?.UserName}"];
            ViewBag.userConnection = UserConnection;

            await addHotelHub.Clients.Client(connectionId: $"{UserConnection}").SendAsync("HotelAdded", newHotelFromReq.Name);

            ViewBag.PendingHotelAddedSuccessfulltToHotels = true;
            return RedirectToAction("PendingHotels");
        }

        public IActionResult HotelDenied(int id)
        {
            //Hotel newHotel = new Hotel()
            //{
            //    Name = DeniedHotelFromReq.Name,
            //    City = DeniedHotelFromReq.City,
            //    CityId = DeniedHotelFromReq.CityId,
            //    Description = DeniedHotelFromReq.Description,
            //    Location = DeniedHotelFromReq.Location,
            //    Manager = DeniedHotelFromReq.Manager,
            //    StarRatig = DeniedHotelFromReq.StarRatig,
            //    NumberOfRooms = DeniedHotelFromReq.NumberOfRooms,
            //    PhoneNumber = DeniedHotelFromReq.PhoneNumber,
            //    ManagerId = DeniedHotelFromReq.ManagerId

            //};

            PendingHotel hotel = pendingHotelRepo.GetById(id);

            hotel.IsDeleted = true;
            hotel.HotelStatus = NewHotelRquestStatus.Denied;
            pendingHotelRepo.Update(hotel);
            pendingHotelRepo.Save();

            ViewBag.PendingHotelAddedSuccessfulltToHotels = false;
            return RedirectToAction("PendingHotels");
        }

        public IActionResult DeniedHotelsList()
        {
            List<PendingHotel> denideHotels = pendingHotelRepo.GetDeniedHotels();
            return View(denideHotels);
        }

        public async Task<IActionResult> MyHotels()
        {
            ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name);

            List<Hotel> hotels = hotelRepo.GetHotelsByManagerId(user.Id);
            return View(hotels);
        }

        public async Task<IActionResult> MyHotel(int id)
        {
            Hotel hotel = hotelRepo.GetById(id);
            City c = cityRepo.GetById(hotel.CityId);
            ViewBag.CityName = c.Name;
            if (hotel != null)
            {
                return View("MyHotel", hotel);
            }
            return NotFound();
        }

        // Actions To Allow Manger To Add , Update and Delete Rooms in their Hotels

        // Add Room 
        [HttpGet]
        [Authorize(Roles = "HotelManager")]

        public async Task<IActionResult> CreateRoom(int hotelId)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var hotel = hotelRepo.GetById(hotelId);
            if (hotel == null || hotel.ManagerId != user.Id)
            {
                return Unauthorized();
            }

            ViewBag.HotelId = hotelId;
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "HotelManager")]

        public async Task<IActionResult> CreateRoom(RoomViewModel model)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var hotel = hotelRepo.GetById(model.HotelId);

            if (hotel == null || hotel.ManagerId != user.Id)
            {
                return Unauthorized();
            }
            if (ModelState.IsValid)
            {
                Room newRoom = new Room
                {
                    RoomNumber = model.RoomNumber,
                    RoomType = (RoomTypes)int.Parse(model.RoomType),
                    Description = model.Description,
                    Capacity = model.Capacity,
                    PricePerNight = model.PricePerNight,
                    HotelId = model.HotelId
                };
                hotel.Rooms.Add(newRoom);
                hotelRepo.Save();
                return RedirectToAction("MyHotel", new { id = model.HotelId });
            }
            ViewBag.HotelId = model.HotelId;
            return View(model);
        }
        // Edit Room 
        [HttpGet]
        [Authorize(Roles = "HotelManager")]

        public async Task<IActionResult> EditRoom(int id)
        {
            var room = await hotelRepo.GetRoomByIdAsync(id);
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var hotel = await hotelRepo.GetHotelByIdAsync(room.HotelId);

            if (room == null || hotel.ManagerId != user.Id)
            {
                return Unauthorized();
            }

            var model = new RoomViewModel
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                RoomType = room.RoomType.ToString(),
                Description = room.Description,
                Capacity = room.Capacity,
                PricePerNight = room.PricePerNight,
                HotelId = room.HotelId
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "HotelManager")]

        public async Task<IActionResult> EditRoom(RoomViewModel model)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            Room room = await hotelRepo.GetRoomByIdAsync(model.Id);
            var hotel = await hotelRepo.GetHotelByIdAsync(room.HotelId);

            if (room == null || hotel.ManagerId != user.Id)
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                room.RoomNumber = model.RoomNumber;
                room.RoomType = (RoomTypes)int.Parse(model.RoomType);
                room.Capacity = model.Capacity;
                room.Description = model.Description;
                room.PricePerNight = model.PricePerNight;

                hotelRepo.Save();

                return RedirectToAction("MyHotel", new { id = room.HotelId });
            }

            return View(model);
        }

        // Delete Room 
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "HotelManager")]

        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await hotelRepo.GetRoomByIdAsync(id);
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var hotel = hotelRepo.GetById(room.HotelId);

            if (room == null || hotel.ManagerId != user.Id)
            {
                return Unauthorized();
            }

            hotelRepo.DeleteRoom(room);
            hotelRepo.Save();

            return RedirectToAction("MyHotel", new { id = room.HotelId });
        }




    }
}
