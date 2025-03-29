using HotelMangementSystem.Models;

namespace HotelMangementSystem.Repositories
{
    public interface IHotelRepo : IGeneralRepo<Hotel>
    {
        public List<Hotel> GetHotels();
        public List<Hotel> GetHotelsByManagerId(string id);
    }
}
