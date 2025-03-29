using HotelMangementSystem.Models;
using HotelMangementSystem.Models.Database;

namespace HotelMangementSystem.Repositories
{
    public class RoomRepo : GeneralRepo<Room>, IRoomRepo
    {
        private readonly DatabaseContext context;

        public RoomRepo(DatabaseContext context) : base(context)
        {
            this.context = context;
        }
        public List<Room> GetRooms()
        {
            return context.Rooms.Where(b => b.IsDeleted == false).ToList();
        }
    }
}
