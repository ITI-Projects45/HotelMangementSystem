using System;
using System.Threading.Tasks;
using HotelMangementSystem.Models;
using HotelMangementSystem.Repositories;
using HotelMangementSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static HotelMangementSystem.Models.Enums.Enums;

namespace HotelMangementSystem.Controllers
{
    public class CartController : Controller
    {
        private readonly IRoomRepo roomRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IReservationRepo reservationRepo;
        private readonly IRoomReservationRepo roomReservationRepo;
        private readonly IBillRepo billRepo;
        private string userId;
        public static HashSet<Room> rooms = new HashSet<Room>();
        public static HashSet<int> roomIds = new HashSet<int>();

        public CartController(IRoomRepo roomRepo, UserManager<ApplicationUser> userManager, IReservationRepo reservationRepo, IRoomReservationRepo roomReservationRepo, IBillRepo billRepo)
        {
            this.roomRepo = roomRepo;
            this.userManager = userManager;
            this.reservationRepo = reservationRepo;
            this.roomReservationRepo = roomReservationRepo;
            this.billRepo = billRepo;
        }

        public async Task<IActionResult> Index()
        {
            rooms.Clear();

            foreach (var item in roomIds)
            {
                Room room = roomRepo.GetByIdWithNoTracking(item);
                rooms.Add(room);

            }
            //string user = User.Identity.Name;
            ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name);

            ReservitionViewModel reservitionViewModel = new ReservitionViewModel()
            {
                Rooms = rooms.ToList(),
                UserId = user.Id,



            };
            ViewBag.UserId = user.Id;
            return View(reservitionViewModel);
        }
        public IActionResult Book(int RoomId)
        {
            roomIds.Add(RoomId);

            //Room room = roomRepo.GetById(RoomId);

            //rooms.Add(room);

            return RedirectToAction("Room", "Rooms", new
            {
                id = RoomId
            });

        }

        public async Task<IActionResult> Booked(ReservitionViewModel reservitionViewModel)
        {
            //if (ModelState.IsValid)
            //{
            //    try
            //    {

            if (ModelState.IsValid)
            {



                ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name);

                foreach (var item in rooms)
                {
                    if (item?.RoomReservation?.Id != null)
                    {
                        reservitionViewModel.Rooms = rooms.ToList();
                        reservitionViewModel.UserId = user.Id;

                        ViewBag.BookedRoom = true;
                        ViewBag.reservedRoomNumber = item.RoomNumber;
                        return View("Index", reservitionViewModel);
                    }

                }

                userId = user.Id;
                reservitionViewModel.Rooms = rooms.ToList();
                reservitionViewModel.UserId = userId;
                reservitionViewModel.BookingDate = DateTime.Now;
                int Sum = 0;
                double vat = 0.1;
                double totalSum = 0;

                foreach (var item in rooms)

                {
                    Sum += item.PricePerNight;
                }
                totalSum = Sum + (Sum * vat);
                Bill NewBill = new Bill()
                {
                    TotalPrice = (int)totalSum,
                    IsDeleted = false,
                    LateCheckout = false,
                    PaymentMethod = PaymentMethods.Cash,
                    RoomCharge = Sum,
                    CheckoutDate = reservitionViewModel.CheckOutDate,
                    ReservistionStatus = ReservistionStatuses.Confirmed,
                };

                billRepo.Insert(NewBill);
                billRepo.Save();




                Reservation newReservation = new Reservation()
                {
                    BillId = NewBill.Id,
                    BookingDate = reservitionViewModel.BookingDate,
                    CheckInDate = reservitionViewModel.CheckInDate,
                    CheckOutDate = reservitionViewModel.CheckOutDate,
                    IsDeleted = false,
                    UserId = userId,
                    User = user,
                    ReservistionStatus = ReservistionStatuses.Confirmed,


                };
                reservationRepo.Insert(newReservation);
                reservationRepo.Save();



                var res = reservationRepo.GetReservationByUserAndBookingDate(user.Id, reservitionViewModel.BookingDate);
                RoomReservation NewRoomReservation = new RoomReservation();
                NewRoomReservation.ReservationId = res.Id;
                NewRoomReservation.RoomId = roomIds.ToList();



                roomReservationRepo.Insert(NewRoomReservation);
                roomReservationRepo.Save();
                foreach (var room in rooms)
                {

                    roomRepo.UpdateRoomStatues(room.Id, NewRoomReservation);
                }

                roomRepo.Save();

                ViewBag.Booked = true;
                rooms.Clear();
                roomIds.Clear();

                return View("Index", reservitionViewModel);
            }
            else
            {
                ModelState.AddModelError("", "An Error is Ocurred");
                return RedirectToAction("Index", reservitionViewModel);
            }

        }

        public static void EmptyLists()
        {
            rooms.Clear();
            roomIds.Clear();
        }

        public IActionResult RemoveFromRoomsList(int RoomId)
        {
            roomIds.Remove(RoomId);
            return RedirectToAction("Index");
        }

    }
}
