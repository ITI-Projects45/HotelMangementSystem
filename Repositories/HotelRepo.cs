using HotelMangementSystem.Models;
using HotelMangementSystem.Models.Database;

namespace HotelMangementSystem.Repositories
{
    public class HotelRepo : GeneralRepo<Hotel>, IHotelRepo
    {
        public HotelRepo(DatabaseContext context) : base(context)
        {
        }
    }
}
