using HotelMangementSystem.Models;

namespace HotelMangementSystem.Repositories
{
    public interface IReservationRepo : IGeneralRepo<Reservation>
    {
        public List<Reservation> GetReservations();
    }
}
