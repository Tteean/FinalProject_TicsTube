using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Services.Implementations;
using FinalProject_ViewModel.ViewModels;
using FinalProject_ViewModel.ViewModels.LoginVm;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinalProject_Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly TicsTubeDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly EmailService _emailService;

        public AccountController(TicsTubeDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, EmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByNameAsync(registerVm.UserName);
            if (user != null)
            {
                ModelState.AddModelError("UserName", "This Username already exists");
                return View();
            }
            user = new()
            {
                FullName = registerVm.FullName,
                UserName = registerVm.UserName,
                Email = registerVm.Email,
                IsSubscribed = registerVm.IsSubscribed,
            };
            if (registerVm.IsSubscribed)
            {
                var subscriber = new Subscriber { Email = registerVm.Email };
                _context.Subscribers.Add(subscriber);
                _context.SaveChanges();
            }
            var result = await _userManager.CreateAsync(user, registerVm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(user, "user");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token.Replace(" ", "+");
            var url = Url.Action("VerifyEmail", "Account", new { email = user.Email, token = token }, Request.Scheme);
            using StreamReader reader = new StreamReader("wwwroot/assets/templates/ConfirmEmail.html");
            var body = reader.ReadToEnd();
            body = body.Replace("{{url}}", url);
            body = body.Replace("{{username}}", user.UserName);
            _emailService.SendEmail(user.Email, "Confirm Email", body);
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.IsInRoleAsync(user, "user"))
                return NotFound();
            await _userManager.ConfirmEmailAsync(user, token);
            return RedirectToAction("login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByNameAsync(loginVm.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(loginVm.UserNameOrEmail);
                if (user == null || !await _userManager.IsInRoleAsync(user, "user"))
                {
                    ModelState.AddModelError("", "username or email is invalid");
                    return View();
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, false, false);
            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError("", "Email is not confirmed");
                return View();
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "account is locked");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Please enter correct values");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVm forgotPasswordVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(forgotPasswordVm.Email);
            if (user == null || !await _userManager.IsInRoleAsync(user, "user"))
            {
                return NotFound();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("VerifyPassword", "Account", new { email = user.Email, token = token }, Request.Scheme);

            using StreamReader reader = new StreamReader("wwwroot/assets/templates/ResetPassword.html");
            var body = reader.ReadToEnd();
            body = body.Replace("{{url}}", url);
            _emailService.SendEmail(user.Email, "Reset Password", body);
            TempData["Success"] = "Email sended to " + user.Email;

            return View(forgotPasswordVm);
        }
        public async Task<IActionResult> VerifyPassword(string token, string email)
        {
            TempData["token"] = token;
            TempData["email"] = email;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.IsInRoleAsync(user, "user"))
                return NotFound();
            if (!await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token))
                return NotFound();
            return RedirectToAction("ResetPassword");
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVm resetPasswordVm)
        {
            TempData["token"] = resetPasswordVm.Token;
            TempData["email"] = resetPasswordVm.Email;
            if (!ModelState.IsValid)
                return View();
            var user = await _userManager.FindByEmailAsync(resetPasswordVm.Email);
            if (user == null || !await _userManager.IsInRoleAsync(user, "user"))
                return View();
            if (!await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetPasswordVm.Token))
                return NotFound();
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordVm.Token, resetPasswordVm.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            return RedirectToAction("Login");
        }
        public IActionResult ExternalLoginByGoogle()
        {
            var redirectUrl = Url.Action("Google", "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(GoogleDefaults.AuthenticationScheme, redirectUrl);
            return new ChallengeResult(GoogleDefaults.AuthenticationScheme, properties);
        }
        public async Task<IActionResult> Google()
        {
            var result = await _signInManager.GetExternalLoginInfoAsync();
            if (result == null)
            {
                return RedirectToAction("Login");
            }
            var email = result.Principal.FindFirstValue(ClaimTypes.Email);
            var userName = result.Principal.FindFirstValue(ClaimTypes.Name);
            var fullName = result.Principal.FindFirstValue(ClaimTypes.Name);


            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new AppUser
                {
                    Email = email,
                    UserName = userName,
                    FullName = fullName
                };
                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, "user");
                await _signInManager.SignInAsync(user, true);
            }
            return RedirectToAction("index", "home");
        }
        [Authorize(Roles = "user")]

        public async Task<IActionResult> Profile(string tab = "dashboard")
        {
            ViewBag.Tab = tab;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("login");
            }
            ProfileVm userProfileVm = new();
            userProfileVm.ProfileUpdateVm = new()
            {
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email
            };
            userProfileVm.Orders = _context.Orders
                .Include(o => o.AppUser)
                .Where(o => o.AppUserId == user.Id).ToList();

            return View(userProfileVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Profile(ProfileUpdateVm profileUpdateVm, string tab = "profile")
        {
            ViewBag.Tab = tab;
            ProfileVm userProfileVm = new();
            userProfileVm.ProfileUpdateVm = profileUpdateVm;

            if (!ModelState.IsValid) return View(userProfileVm);
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("login");
            user.FullName = profileUpdateVm.FullName;
            user.Email = profileUpdateVm.Email;
            user.UserName = profileUpdateVm.UserName;


            if (profileUpdateVm.NewPassword != null)
            {
                if (profileUpdateVm.CurrentPassword == null)
                {
                    ModelState.AddModelError("CurrentPassword", "Current password is required");
                    return View(userProfileVm);

                }
                var response = await _userManager.ChangePasswordAsync(user, profileUpdateVm.CurrentPassword, profileUpdateVm.NewPassword);
                if (!response.Succeeded)
                {
                    foreach (var error in response.Errors)
                        ModelState.AddModelError("", error.Description);

                    return View(userProfileVm);
                }
            }
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(userProfileVm);
            }
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("index", "home");

        }
    } 
}
