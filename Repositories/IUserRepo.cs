using HotelMangementSystem.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HotelMangementSystem.Repositories
{
    public interface IUserRepo
    {
        Task<ApplicationUser?> GetCurrentUserAsync(HttpContext httpContext);
        Task UpdateUserAsync(ApplicationUser user, IFormFile? profileImage);
    }
}
