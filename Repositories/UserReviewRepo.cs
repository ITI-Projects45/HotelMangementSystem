using HotelMangementSystem.Models;
using HotelMangementSystem.Models.Database;

namespace HotelMangementSystem.Repositories
{
    public class UserReviewRepo : GeneralRepo<UserReview>, IUserReviewRepo
    {
        public UserReviewRepo(DatabaseContext context) : base(context)
        {
        }
    }
}
