using HotelMangementSystem.Models;
using HotelMangementSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelMangementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }



        #region Register
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegistrationViewModel RegisterForm)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser appFromDb = await userManager.FindByEmailAsync(RegisterForm.Email);
                if (appFromDb == null)
                {
                    ApplicationUser newUser = new ApplicationUser()
                    {
                        UserName = RegisterForm.UserName,
                        Email = RegisterForm.Email,
                        PhoneNumber = RegisterForm.PhoneNumber,
                        PasswordHash = RegisterForm.Password
                    };

                    if (userManager.Users.Count() == 0)
                    {
                        ApplicationRole role = new ApplicationRole()
                        {
                            Name = "TopAdmin"
                        };

                        IdentityResult roleResult = await roleManager.CreateAsync(role);


                        if (roleResult.Succeeded)
                        {
                            IdentityResult UserResult = await userManager.CreateAsync(newUser, RegisterForm.Password);
                            if (UserResult.Succeeded)
                            {
                                await userManager.AddToRoleAsync(newUser, "TopAdmin");
                                await signInManager.SignInAsync(newUser, false);
                                return RedirectToAction("Index", "Home");
                            }
                            foreach (var item in UserResult.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                        foreach (var item in roleResult.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }


                    }
                    else
                    {
                        IdentityResult UserResult = await userManager.CreateAsync(newUser, RegisterForm.Password);
                        if (UserResult.Succeeded)
                        {
                            await signInManager.SignInAsync(newUser, false);
                            return RedirectToAction("Index", "Home");
                        }
                        foreach (var item in UserResult.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email is already taken.");
                }
            }




            return View("Register", RegisterForm);
        }



        #endregion


        #region Logout

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion


    }
}
