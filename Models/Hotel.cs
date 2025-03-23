using System.ComponentModel.DataAnnotations.Schema;

namespace HotelMangementSystem.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StarRatig { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public int NumberOfRooms { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Manager")]
        public string ManagerId { get; set; }


        [ForeignKey("City")]

        public int CityId { get; set; }


        public ApplicationUser? Manager { get; set; }
        public City? City { get; set; }
        public List<Room>? Rooms { get; set; }
        public List<Review> Reviews { get; set; }


    }
}
