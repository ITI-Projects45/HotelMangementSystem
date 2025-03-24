using HotelMangementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelMangementSystem.Controllers
{
    //[Authorize(Roles = "TopAdmin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult AddNewRole()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddAdminRole()
        
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdminRole(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                ModelState.AddModelError("", "Email cannot be empty.");
                return View();
            }
            var user = await userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View();
            }
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = "Admin" });
            }
            var result = await userManager.AddToRoleAsync(user, "Admin");
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = $"User {Email} has been assigned as Admin.";
                return RedirectToAction("RoleList");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("AddAdminRole");
        }
    }
}