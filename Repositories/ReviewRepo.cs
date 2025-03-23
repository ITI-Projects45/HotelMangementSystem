using HotelMangementSystem.Models;
using HotelMangementSystem.Models.Database;

namespace HotelMangementSystem.Repositories
{
    public class ReviewRepo : GeneralRepo<Review>, IReviewRepo
    {
        public ReviewRepo(DatabaseContext context) : base(context)
        {
        }
    }
}
