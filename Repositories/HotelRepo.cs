using HotelMangementSystem.Models;
using HotelMangementSystem.Models.Database;

namespace HotelMangementSystem.Repositories
{
    public class HotelRepo : GeneralRepo<Hotel>, IHotelRepo
    {
        private readonly DatabaseContext context;

        public HotelRepo(DatabaseContext context) : base(context)
        {
            this.context = context;
        }
        public List<Hotel> GetHotels()
        {
            return context.Hotels.Where(b => b.IsDeleted == false).ToList();
        }
        public List<Hotel> GetHotelsByManagerId(string id)
        {
            return context.Hotels.Where(b => b.IsDeleted == false && b.ManagerId.Contains(id)).ToList();

        }
    }
}
