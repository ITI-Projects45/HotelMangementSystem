using HotelMangementSystem.Models;

namespace HotelMangementSystem.Repositories
{
    public interface IHotelRepo : IGeneralRepo<Hotel>
    {
        Task<List<Hotel>> GetHotelsByCityAsync(string cityName, int page, int pageSize);
        Task<int> GetTotalHotelsCountAsync(string cityName);
        Task<List<string>> GetAllCitiesAsync();
    }
}
