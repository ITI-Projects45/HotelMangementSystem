using System.ComponentModel.DataAnnotations.Schema;

namespace HotelMangementSystem.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int StarNumber { get; set; }
        public string Content { get; set; }
        public DateTime ReviewDate { get; set; } // Fixed value in seeding
        public bool IsDeleted { get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public List<UserReview> UserReviews { get; set; }
    }
}
