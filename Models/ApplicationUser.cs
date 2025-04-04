using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HotelMangementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        public string? ProfilePictureURL { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public List<Review>? Reviews { get; set; }
        public List<Hotel>? Hotel { get; set; }
        public List<Reservation>? Reservations { get; set; }
        public UserReview? UserReview { get; set; }


    }
}
