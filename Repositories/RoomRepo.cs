using HotelMangementSystem.Models;
using HotelMangementSystem.Models.Database;
using HotelMangementSystem.Models.Enums;
using Microsoft.EntityFrameworkCore;
using static HotelMangementSystem.Models.Enums.Enums;

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
        public async Task<List<Room>> SearchRoomsAsync(int cityId, RoomTypes roomType)
        {
            try
            {
                return await context.Rooms
                    .AsNoTracking()
                    .Include(r => r.Hotel)
                    .ThenInclude(h => h.City)
                    .Where(r => !r.IsDeleted &&
                                r.RoomType == roomType &&
                                r.Hotel.CityId == cityId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching rooms: {ex.Message}");
                return new List<Room>(); 
            }
        }

    }
}
