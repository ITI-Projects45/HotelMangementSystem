using HotelMangementSystem.Models;
using HotelMangementSystem.Models.Database;

namespace HotelMangementSystem.Repositories
{
    public class ReservationRepo : GeneralRepo<Reservation>, IReservationRepo
    {
        private readonly DatabaseContext context;

        public ReservationRepo(DatabaseContext context) : base(context)
        {
            this.context = context;
        }
        public List<Reservation> GetReservations()
        {
            return context.Reservations.Where(b => b.IsDeleted == false).ToList();
        }

        public Reservation GetReservationByUserAndBookingDate(string userId, DateTime BookingDate)
        {
            return context.Reservations.FirstOrDefault(r => r.UserId.Contains(userId) && r.BookingDate == BookingDate);
        }
    }
}
