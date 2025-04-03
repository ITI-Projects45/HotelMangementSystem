﻿using HotelMangementSystem.Models;
using static HotelMangementSystem.Models.Enums.Enums;

namespace HotelMangementSystem.Repositories
{
    public interface IRoomRepo : IGeneralRepo<Room>
    {
        public List<Room> GetRooms();
        public void SoftDelete(Room room);

        Task<List<Room>> SearchRoomsAsync(int cityId, RoomTypes roomType);
    }
}
