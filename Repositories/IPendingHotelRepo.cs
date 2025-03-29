using HotelMangementSystem.Models;

namespace HotelMangementSystem.Repositories
{
    public interface IPendingHotelRepo : IGeneralRepo<PendingHotel>
    {
        public PendingHotel CheckIfExists(string HotelName, string ManagerID);
        public PendingHotel GetRequestByManagerID(string ManagerID);

    }
}
