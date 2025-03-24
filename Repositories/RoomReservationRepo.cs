using HotelMangementSystem.Models;
using HotelMangementSystem.Models.Database;

namespace HotelMangementSystem.Repositories
{
    public class RoomReservationRepo : GeneralRepo<RoomReservation>, IRoomReservationRepo
    {
        public RoomReservationRepo(DatabaseContext context) : base(context)
        {
        }
    }
}
