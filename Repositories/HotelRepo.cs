﻿using HotelMangementSystem.Models;
using HotelMangementSystem.Models.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelMangementSystem.Repositories
{
    public class HotelRepo : GeneralRepo<Hotel>, IHotelRepo
    {
        private readonly DatabaseContext context;

        public HotelRepo(DatabaseContext context) : base(context)
        {
            this.context = context;
        }
        public List<Hotel> GetHotels()
        {
            return context.Hotels.Where(b => b.IsDeleted == false).ToList();
        }
        public List<Hotel> GetHotelsByManagerId(string id)
        {
            return context.Hotels.Where(b => b.IsDeleted == false && b.ManagerId.Contains(id)).ToList();


        }
        public async Task<List<Hotel>> GetHotelsByCityAsync(string cityName, int page, int pageSize)
        {
            var query = context.Hotels
                .Include(h => h.City)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(cityName))
            {
                query = query.Where(h => h.City.Name == cityName);
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> GetTotalHotelsCountAsync(string cityName)
        {
            var query = context.Hotels
                .Include(h => h.City)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(cityName))
            {
                query = query.Where(h => h.City.Name == cityName);
            }

            return await query.CountAsync();
        }
        public async Task<List<string>> GetAllCitiesAsync()
        {
            return await context.Cities
                .Select(c => c.Name)
                .Distinct()
                .ToListAsync();
        }
        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            return await context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefaultAsync(h => h.Id == id);
        }
        public List<Hotel> GetFourTopRatedRandomizedHotels()
        {
            List<Hotel> hotels = context.Hotels.Where(h => h.IsDeleted == false && h.StarRatig > 4).OrderBy(h => Guid.NewGuid()).Take(4).ToList();
            return hotels;
        }

        public async Task DeleteRoom(Room room, int HotelId)
        {
            Hotel hotel = await GetHotelWithRoomsAsync(HotelId);
            hotel.Rooms.Remove(room);
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            return await context.Rooms.Include(r => r.Hotel).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Hotel> GetHotelWithRoomsAsync(int id)
        {
            return await context.Hotels
              .Include(h => h.Rooms.Where(r => r.IsDeleted == false))
              .FirstOrDefaultAsync(h => h.Id == id && h.IsDeleted == false);
        }



    }
}
