using static HotelMangementSystem.Models.Enums.Enums;

namespace HotelMangementSystem.ViewModels
{
    public class BillViewModel
    {
        public int Id { get; set; }
        public int RoomCharge { get; set; }
        public bool LateCheckout { get; set; }
        public DateTime CheckoutDate { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
        public int TotalPrice { get; set; }
        public ReservistionStatuses ReservistionStatus { get; set; }
    }
}
