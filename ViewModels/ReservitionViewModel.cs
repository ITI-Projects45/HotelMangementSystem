using HotelMangementSystem.Models;
using static HotelMangementSystem.Models.Enums.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelMangementSystem.ViewModels
{
    public class ReservitionViewModel
    {
        public DateTime BookingDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        //public int Deposite { get; set; }
        public ReservistionStatuses ReservistionStatus { get; set; }


        public bool IsDeleted { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Bill")]
        public int BillId { get; set; }


        public List<Room>? Rooms { get; set; }



        public Bill? Bill { get; set; }
        public ApplicationUser? User { get; set; }

        public RoomReservation? RoomReservation { get; set; }
    }
}
