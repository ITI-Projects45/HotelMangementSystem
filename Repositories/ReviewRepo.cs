using System.Linq;
using HotelMangementSystem.Models;
using HotelMangementSystem.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace HotelMangementSystem.Repositories
{
    public class ReviewRepo : GeneralRepo<Review>, IReviewRepo
    {
        private readonly DatabaseContext context;

        public ReviewRepo(DatabaseContext context) : base(context)
        {
            this.context = context;
        }
        public List<Review> getAllWithUser()
        {
            return context.Reviews.Include("User").ToList();

        }
        public List<Review> getAllWithUserAndHotels()
        {
            return context.Reviews.Include("User").Include("Hotel").Where(r => r.IsDeleted == true).ToList();

        }
    }
}
