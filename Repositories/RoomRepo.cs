using HotelMangementSystem.Models;
using HotelMangementSystem.Models.Database;

namespace HotelMangementSystem.Repositories
{
    public class RoomRepo : GeneralRepo<Room>, IRoomRepo
    {
        public RoomRepo(DatabaseContext context) : base(context)
        {
        }
    }
}
