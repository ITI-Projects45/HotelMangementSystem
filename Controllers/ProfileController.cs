using System.Threading.Tasks;
using HotelMangementSystem.Repositories;
using HotelMangementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HotelMangementSystem.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserRepo userRepo;
        public ProfileController(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }
        public async Task<IActionResult> Index()
        {
            var user = await userRepo.GetCurrentUserAsync(HttpContext);
            if (user == null)
            {
                return NotFound();
            }
            return View("Index", user);

        }
        public async Task<IActionResult> Edit()
        {

            var user = await userRepo.GetCurrentUserAsync(HttpContext);
            if (user == null)
            {
                return NotFound();
            }
            var userViewModel = new ProfileViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CurrentProfilePictureUrl = user.ProfilePictureURL,
            };

            return View("Edit", userViewModel);
        }
        public async Task<IActionResult> Edit(ProfileViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await userRepo.GetCurrentUserAsync(HttpContext);
            if (user == null) { return NotFound(); }
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            await userRepo.UpdateUserAsync(user, model.ProfileImage);
            return RedirectToAction("Index");

        }


    }
}
