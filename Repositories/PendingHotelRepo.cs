using HotelMangementSystem.Models;
using HotelMangementSystem.Models.Database;

namespace HotelMangementSystem.Repositories
{
    public class PendingHotelRepo : GeneralRepo<PendingHotel>, IPendingHotelRepo
    {
        private readonly DatabaseContext context;

        public PendingHotelRepo(DatabaseContext context) : base(context)
        {
            this.context = context;
        }
        public PendingHotel CheckIfExists(string HotelName, string ManagerID)
        {
            PendingHotel hotel = context.PendingHotels.FirstOrDefault(h => h.Name.Contains(HotelName) && h.ManagerId.Contains(ManagerID));
            return hotel;
        }
        public PendingHotel GetRequestByManagerID(string ManagerID)
        {
            PendingHotel hotel = context.PendingHotels.First(h => h.ManagerId.Contains(ManagerID));
            return hotel;
        }
    }
}
