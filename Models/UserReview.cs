using System.ComponentModel.DataAnnotations.Schema;

namespace HotelMangementSystem.Models
{
    public class UserReview
    {
        public int Id { get; set; }

        [ForeignKey("Review")]
        public int ReviewId { get; set; }
        public Review? Review { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
