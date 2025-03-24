using HotelMangementSystem.Models;
using HotelMangementSystem.Models.Database;

namespace HotelMangementSystem.Repositories
{
    public class BillRepo : GeneralRepo<Bill>, IBillRepo
    {
        public BillRepo(DatabaseContext context) : base(context)
        {

        }
    }
}
