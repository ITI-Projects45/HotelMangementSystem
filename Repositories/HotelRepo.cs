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
    }
}
