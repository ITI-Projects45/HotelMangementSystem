using System.ComponentModel.DataAnnotations;
using System.Configuration;
using static HotelMangementSystem.Models.Enums.Enums;

namespace HotelMangementSystem.ViewModels
{

    public class RoomViewModel
    {
        public int Id { get; set; }
        [IntegerValidator(MinValue = 1)]
        public int RoomNumber { get; set; }
        public RoomTypes RoomType { get; set; }
        public string Description { get; set; }
        [Range(1, 10)]

        public int Capacity { get; set; }
        [IntegerValidator(MinValue = 150)]

        public int PricePerNight { get; set; }
        public int HotelId { get; set; }
        public RoomStatuses roomStatus { get; set; } = RoomStatuses.Available;

    }


}
