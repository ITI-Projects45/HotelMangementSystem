using HotelMangementSystem.Models;
using HotelMangementSystem.Models.Database;

namespace HotelMangementSystem.Repositories
{
    public class CityRepo : GeneralRepo<City>, ICityRepo
    {
        public CityRepo(DatabaseContext context) : base(context)
        {
        }
    }
}
