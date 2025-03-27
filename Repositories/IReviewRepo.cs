using HotelMangementSystem.Models;

namespace HotelMangementSystem.Repositories
{
    public interface IReviewRepo : IGeneralRepo<Review>
    {
        public List<Review> getAllWithUser();
        public List<Review> getAllWithUserAndHotels();
    }
}
