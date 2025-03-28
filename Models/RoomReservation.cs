using System.ComponentModel.DataAnnotations.Schema;

namespace HotelMangementSystem.Models
{
    public class RoomReservation
    {
        public int Id { get; set; }

        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }
        public Reservation? Reservation { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room? Room { get; set; }
    }
}
