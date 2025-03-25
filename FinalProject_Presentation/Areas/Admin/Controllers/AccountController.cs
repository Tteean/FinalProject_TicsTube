using FinalProject_Core.Models;
using FinalProject_Presentation.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject_Presentation.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManger, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManger = userManger;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> CreateAdminUser()
        {
            AppUser user = new AppUser()
            {
                FullName = "admin",
                UserName = "_admin",
                Email = "admin@gmail.com"
            };
            IdentityResult result = await _userManger.CreateAsync(user, "Admin123_");
            await _userManger.AddToRoleAsync(user, "admin");
            return Json(result);
        }
        public async Task<IActionResult> CreateRole()
        {
            await _roleManager.CreateAsync(new IdentityRole("admin"));
            await _roleManager.CreateAsync(new IdentityRole("member"));
            await _roleManager.CreateAsync(new IdentityRole("moderator"));
            return Content("added");

        }
        public IActionResult Login()

        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginVm adminLoginVm, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userManger.FindByNameAsync(adminLoginVm.UserName);
            if (user == null || (!await _userManger.IsInRoleAsync(user, "admin") && !await _userManger.IsInRoleAsync(user, "moderator")))
            {
                ModelState.AddModelError("", "UserName or Password is incorrect");
                return View();
            }
            var result = await _userManger.CheckPasswordAsync(user, adminLoginVm.Password);
            if (!result)
            {
                ModelState.AddModelError("", "UserName or Password is incorrect");
                return View();
            }
            await _signInManager.SignInAsync(user, false);
            return returnUrl != null ? Redirect(returnUrl) : RedirectToAction("Index", "Dashboard");
        }
        [Authorize(Roles = "admin,moderator")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> GetUser()
        {
            var user = await _userManger.GetUserAsync(User);
            return Json(User.Identity);
        }


    }
}

