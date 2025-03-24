using HotelMangementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelMangementSystem.Controllers
{
    [Authorize(Roles = "TopAdmin")]
    public class RoleController : Controller
    {
        RoleManager<ApplicationRole> roleManager;
        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager;
        }


        public IActionResult AddNewRole()
        {
            return View("AddNewRole");
        }
    }
}
