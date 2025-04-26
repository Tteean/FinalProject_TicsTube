using FinalProject_Core.Enum;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.BasketDtos;
using FinalProject_Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_Presentation.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly TicsTubeDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        public BasketController(IBasketService basketService, TicsTubeDbContext context, UserManager<AppUser> userManager)
        {
            _basketService = basketService;
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var basketItems = await _basketService.GetBasketAsync();
            return View(basketItems);
        }

        [Authorize(Roles = "member")]
        public async Task<IActionResult> Checkout()
        {
            var checkoutDto = await _basketService.GetCheckoutAsync();
            return View(checkoutDto);
        }

        [HttpPost]
        [Authorize(Roles = "member")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(OrderCreateDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                var checkoutDto = await _basketService.GetCheckoutAsync();
                return View(checkoutDto);
            }

            await _basketService.SubmitOrderAsync(orderDto);
            return RedirectToAction("Profile", "Account", new { tab = "orders" });
        }

        public async Task<IActionResult> Add(int? id)
        {
            if (id == null) return NotFound();

            var basketItems = await _basketService.AddToBasketAsync(id.Value);
            return PartialView("_BasketPartial", basketItems);
        }

        public async Task<IActionResult> RemoveItem(int? id)
        {
            if (id == null) return NotFound();

            var basketItems = await _basketService.RemoveFromBasketAsync(id.Value);
            return PartialView("_BasketPartial", basketItems);
        }

        [Authorize(Roles = "member")]
        public async Task<IActionResult> GetOrderItems(int orderId)
        {
            var order = _context.Orders
                .Where(o => o.AppUserId == _userManager.GetUserId(User))
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == orderId);
            return PartialView("_OrderItemsPartial", orderId);
        }

        [Authorize(Roles = "member")]
        public async Task<IActionResult> Cancel(int orderId)
        {
            var order = _context.Orders
                .Where(o => o.AppUserId == _userManager.GetUserId(User))
                .FirstOrDefault(o => o.Id == orderId);
            order.OrderStatus = OrderStatus.Cancelled;
            _context.SaveChanges();
            return RedirectToAction("Profile", "Account", new { tab = "orders" });
        }
    }
}
