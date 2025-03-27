using static HotelMangementSystem.Models.Enums.Enums;

namespace HotelMangementSystem.ViewModels
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Deposit { get; set; }
        public ReservistionStatuses ReservistionStatus { get; set; }
        public string? UserId { get; set; }
    }
}
