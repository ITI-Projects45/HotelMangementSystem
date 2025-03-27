using static HotelMangementSystem.Models.Enums.Enums;

namespace HotelMangementSystem.ViewModels
{
    public class RoomReservationViewModel
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int RoomId { get; set; }
    }
}
