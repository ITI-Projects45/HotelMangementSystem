using System.ComponentModel.DataAnnotations.Schema;

namespace HotelMangementSystem.Models
{
    public class UserReview
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }



        public List<Review>? Reviews { get; set; }
        public ApplicationUser? User { get; set; }

    }
}
