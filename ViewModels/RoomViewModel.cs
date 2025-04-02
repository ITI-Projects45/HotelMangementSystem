using static HotelMangementSystem.Models.Enums.Enums;

namespace HotelMangementSystem.ViewModels
{

    public class RoomViewModel
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public int PricePerNight { get; set; }
        public int HotelId { get; set; }
    }


}
