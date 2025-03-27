namespace HotelMangementSystem.ViewModels
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public int StarNumber { get; set; }
        public string? Content { get; set; }
        public DateTime ReviewDate { get; set; }
        public int HotelId { get; set; }
    }
}
