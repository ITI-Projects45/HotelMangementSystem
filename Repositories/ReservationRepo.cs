using HotelMangementSystem.Models;
using HotelMangementSystem.Models.Database;

namespace HotelMangementSystem.Repositories
{
    public class ReservationRepo : GeneralRepo<Reservation>, IReservationRepo
    {
        public ReservationRepo(DatabaseContext context) : base(context)
        {
        }
    }
}
