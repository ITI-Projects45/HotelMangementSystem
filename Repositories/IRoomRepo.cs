﻿using HotelMangementSystem.Models;

namespace HotelMangementSystem.Repositories
{
    public interface IRoomRepo : IGeneralRepo<Room>
    {
        public List<Room> GetRooms();
    }
}
