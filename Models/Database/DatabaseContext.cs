using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static HotelMangementSystem.Models.Enums.Enums;

namespace HotelMangementSystem.Models.Database
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<Bill> Bills { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<RoomReservation> RoomReservations { get; set; }
        public DbSet<UserReview> UserReviews { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed Cities
            builder.Entity<City>().HasData(
                new City { Id = 1, Name = "Cairo", Country = "Egypt", IsDeleted = false },
                new City { Id = 2, Name = "Alexandria", Country = "Egypt", IsDeleted = false },
                new City { Id = 3, Name = "Giza", Country = "Egypt", IsDeleted = false }
            );

            // Precomputed Password Hashes
            string managerId = "a1b2c3d4-e5f6-7890-abcd-1234567890ef";
            string userId1 = "11111111-2222-3333-4444-555555555555";
            string userId2 = "66666666-7777-8888-9999-000000000000";

            string managerHash = "AQAAAAEAACcQAAAAEN4u5v1Jxyz12345==";
            string user1Hash = "AQAAAAEAACcQAAAAEMuBNaXYZ67890==";
            string user2Hash = "AQAAAAEAACcQAAAAEMeV7MnABC98765==";

            // Seed Users
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = managerId,
                    UserName = "hotelmanager",
                    NormalizedUserName = "HOTELMANAGER",
                    Email = "manager@example.com",
                    NormalizedEmail = "MANAGER@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = managerHash
                },
                new ApplicationUser
                {
                    Id = userId1,
                    UserName = "user1",
                    NormalizedUserName = "USER1",
                    Email = "user1@example.com",
                    NormalizedEmail = "USER1@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = user1Hash
                },
                new ApplicationUser
                {
                    Id = userId2,
                    UserName = "user2",
                    NormalizedUserName = "USER2",
                    Email = "user2@example.com",
                    NormalizedEmail = "USER2@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = user2Hash
                }
            );

            // Seed Hotels
            builder.Entity<Hotel>().HasData(
                new Hotel { Id = 1, Name = "Hotel Cleopatra", Description = "Luxury accommodation in Cairo.", StarRating = 5, Location = "Downtown", PhoneNumber = "0123456789", NumberOfRooms = 200, IsDeleted = false, ManagerId = managerId, CityId = 1 },
                new Hotel { Id = 2, Name = "Hotel Alexandria Pearl", Description = "Beachside hotel with stunning views.", StarRating = 4, Location = "Seafront", PhoneNumber = "0223456789", NumberOfRooms = 150, IsDeleted = false, ManagerId = managerId, CityId = 2 }
            );

            // Seed Rooms
            builder.Entity<Room>().HasData(
                new Room { Id = 1, RoomNumber = 101, RoomType = RoomTypes.Single, Description = "Cozy single room.", PricePerNight = 50, Capacity = 1, RoomStatus = (RoomStatuses)2, IsDeleted = false, HotelId = 1 },
                new Room { Id = 2, RoomNumber = 102, RoomType = RoomTypes.Double, Description = "Spacious double room.", PricePerNight = 100, Capacity = 2, RoomStatus = (RoomStatuses)1, IsDeleted = false, HotelId = 1 }
            );

            // Seed Reviews
            builder.Entity<Review>().HasData(
                new Review
                {
                    Id = 1,
                    StarNumber = 5,
                    Content = "Amazing experience!",
                    ReviewDate = new DateTime(2025, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false,
                    HotelId = 1,
                    UserId = userId1
                },
                new Review
                {
                    Id = 2,
                    StarNumber = 4,
                    Content = "Very good service.",
                    ReviewDate = new DateTime(2025, 3, 2, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false,
                    HotelId = 1,
                    UserId = userId2
                }
            );

            // Seed Reservations
            builder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 1,
                    BookingDate = new DateTime(2025, 3, 10, 0, 0, 0, DateTimeKind.Utc),
                    CheckInDate = new DateTime(2025, 3, 15, 0, 0, 0, DateTimeKind.Utc),
                    CheckOutDate = new DateTime(2025, 3, 20, 0, 0, 0, DateTimeKind.Utc),
                    Deposite = 500,
                    ReservistionStatus = (ReservistionStatuses)1,
                    IsDeleted = false,
                    UserId = userId1
                }
            );

            // Seed Bills
            builder.Entity<Bill>().HasData(
                new Bill
                {
                    Id = 1,
                    RoomCharge = 500,
                    LateCheckout = false,
                    CheckoutDate = new DateTime(2025, 3, 20, 0, 0, 0, DateTimeKind.Utc),
                    PaymentMethod = (PaymentMethods)1,
                    TotalPrice = 1500,
                    ReservistionStatus = (ReservistionStatuses)2,
                    IsDeleted = false,
                    ReservationId = 1
                }
            );

            // Seed Room Reservations
            builder.Entity<RoomReservation>().HasData(
                new RoomReservation
                {
                    Id = 1,
                    ReservationId = 1,
                    RoomId = 1
                }
            );

            // Seed UserReviews (Ensuring referenced Users and Reviews exist)
            builder.Entity<UserReview>().HasData(
                new UserReview
                {
                    Id = 1,
                    ReviewId = 1, // Must exist in Review seeding
                    UserId = userId1 // Must exist in ApplicationUser seeding
                },
                new UserReview
                {
                    Id = 2,
                    ReviewId = 2,
                    UserId = userId2
                }
            );

            // Configure Relationships
            builder.Entity<UserReview>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserReviews)
                .HasForeignKey(ur => ur.UserId);

            builder.Entity<UserReview>()
                .HasOne(ur => ur.Review)
                .WithMany(r => r.UserReviews)
                .HasForeignKey(ur => ur.ReviewId);
        }

    }
}