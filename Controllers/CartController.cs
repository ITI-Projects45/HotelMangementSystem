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
                Room room = roomRepo.GetById(item);
                rooms.Add(room);

            }
            //string user = User.Identity.Name;
            ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name);

            ReservitionViewModel reservitionViewModel = new ReservitionViewModel()
            {
                Rooms = rooms.ToList(),
                UserId = user.Id,



            };
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

        public async Task<IActionResult> BookedAsync(ReservitionViewModel reservitionViewModel)
        {
            ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name);



            userId = user.Id;
            reservitionViewModel.Rooms = rooms.ToList();
            reservitionViewModel.UserId = userId;
            reservitionViewModel.BookingDate = DateTime.Now;
            Bill NewBill = new Bill()
            {
                TotalPrice = 15,
                IsDeleted = false,
                LateCheckout = false,
                PaymentMethod = PaymentMethods.InstaPay,
                RoomCharge = 150,
                CheckoutDate = reservitionViewModel.CheckOutDate,
                ReservistionStatus = ReservistionStatuses.Confirmed
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



            /*-----------------------------------------*/

            //foreach (Room room in rooms)
            //{
            //    room.roomStatus = RoomStatuses.NotAvailable;
            //    roomRepo.Update(room);
            //    roomRepo.Save();
            //}



            /*---------------------------------------*/


            var res = reservationRepo.GetReservationByUserAndBookingDate(user.Id, reservitionViewModel.BookingDate);

            RoomReservation NewRoomReservation = new RoomReservation()
            {
                ReservationId = res.Id,
                //RoomIds = roomIds.ToList()
            };
            roomReservationRepo.Insert(NewRoomReservation);
            roomReservationRepo.Save();


            ViewBag.Booked = true;


            return View("Index", reservitionViewModel);

        }
    }
}
