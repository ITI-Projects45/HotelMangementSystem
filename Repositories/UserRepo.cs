using HotelMangementSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HotelMangementSystem.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        public UserRepo(UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.environment = environment;
        }

        public async Task<ApplicationUser?> GetCurrentUserAsync(HttpContext httpContext)
        {

            return await userManager.GetUserAsync(httpContext.User);

        }

        public async Task UpdateUserAsync(ApplicationUser user, IFormFile? profileImage)
        {
            if (profileImage != null && profileImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(environment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(profileImage.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profileImage.CopyToAsync(stream);
                }
                user.ProfilePictureURL = "/uploads/" + uniqueFileName;
            }
            await userManager.UpdateAsync(user);
        }
    }
}
