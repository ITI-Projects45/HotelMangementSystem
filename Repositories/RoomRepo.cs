using HotelMangementSystem.Models;
using HotelMangementSystem.Models.Database;
using HotelMangementSystem.Models.Enums;
using Microsoft.EntityFrameworkCore;

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
        public Room GetById(int id)
        {
            return context.Rooms.Include(r => r.Hotel).Include(r => r.HotelId).FirstOrDefault(r => r.Id == id);
        }
        public void SoftDelete(Room room)
        {
            room.IsDeleted = true;
            room.roomStatus = Enums.RoomStatuses.NotAvailable;
        }


    }
}
