using Microsoft.AspNetCore.Identity;

namespace HotelMangementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Review>? Reviews { get; set; }
        public Hotel? Hotel { get; set; }
        public List<Reservation>? Reservations { get; set; }
        public List<UserReview>? UserReviews { get; set; }
    }
}
