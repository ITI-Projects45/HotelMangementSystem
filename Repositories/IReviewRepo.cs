using HotelMangementSystem.Models;

namespace HotelMangementSystem.Repositories
{
    public interface IReviewRepo : IGeneralRepo<Review>
    {
        public List<Review> getAllWithUser();
        public List<Review> getAllWithUserAndHotels();
        public List<Review> GetReviews();
        public List<Review> getSevenRandomReviewsWithUserAndHotels();
    }
}
