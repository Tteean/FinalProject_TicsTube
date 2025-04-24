using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Extentions;
using FinalProject_Service.Services.Interfaces;
using FinalProject_ViewModel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPalCheckoutSdk.Orders;

namespace FinalProject_Presentation.Controllers
{
    public class SubscribeController : Controller
    {
        private readonly PayPalClient _сlient;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TicsTubeDbContext _context;

        public SubscribeController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, PayPalClient сlient, TicsTubeDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _сlient = сlient;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateOrder(string plan)
        {
            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(new OrderRequest
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>
            {
                new PurchaseUnitRequest
                {
                    AmountWithBreakdown = new AmountWithBreakdown
                    {
                        CurrencyCode = "USD",
                        Value = plan == "year_member" ? "14.99" :
                                plan == "halfYear_member" ? "12.99" : "0.10"
                    }
                }
            },
                ApplicationContext = new ApplicationContext
                {
                    ReturnUrl = Url.Action("Success", "Subscription", new { plan }, Request.Scheme),
                    CancelUrl = Url.Action("Cancel", "Subscription", null, Request.Scheme)
                }
            });

            var client = _сlient.Client;
            var response = await client.Execute(request);
            var result = response.Result<Order>();
            var approvalLink = result.Links.First(x => x.Rel == "approve").Href;

            return Redirect(approvalLink);
        }
        public async Task<IActionResult> Success(string token, string plan)
        {
            var captureRequest = new OrdersCaptureRequest(token);
            captureRequest.RequestBody(new OrderActionRequest());
            var response = await _сlient.Client.Execute(captureRequest);

            var user = await _userManager.GetUserAsync(User);
            if (!await _roleManager.RoleExistsAsync(plan))
            {
                await _roleManager.CreateAsync(new IdentityRole(plan));
            }
            await _userManager.AddToRoleAsync(user, plan);
            var now = DateTime.UtcNow;
            var duration = plan switch
            {
                "month_member" => TimeSpan.FromDays(30),
                "halfYear_member" => TimeSpan.FromDays(180),
                "year_member" => TimeSpan.FromDays(365),
                _ => TimeSpan.FromDays(30)
            };

            var subscription = new Subscription
            {
                UserId = user.Id,
                Plan = plan,
                StartDate = now,
                EndDate = now.Add(duration)
            };

            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Cancel()
        {
            return View("Cancel");
        }
    }
}
