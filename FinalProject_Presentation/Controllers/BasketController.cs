using FinalProject_Core.Enum;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.BasketDtos;
using FinalProject_Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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
            var basket = HttpContext.Request.Cookies["basket"];
            List<BasketItemDto> basketItemDtos;

            if (basket == null)
            {
                basketItemDtos = new();
            }
            else
            {
                basketItemDtos = JsonSerializer.Deserialize<List<BasketItemDto>>(basket);
            }

            return View(basketItemDtos);
        }

        [Authorize(Roles = "user")]
        public async Task<IActionResult> Checkout()
        {
            var user = _userManager.Users
                .Include(u => u.BasketItems)
                .ThenInclude(ub => ub.Product)
                .FirstOrDefault(u => u.UserName == User.Identity.Name);
            CheckoutDto checkoutDto = new();
            checkoutDto.CheckoutItemDtos = user.BasketItems.Select(b => new CheckoutItemDto
            {
                Name = b.Product.Name,
                TotalItemPrice = b.Product.CostPrice * b.Count,
                Count = b.Count,
            }).ToList();
            return View(checkoutDto);
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(OrderDto orderDto)
        {
            var username = User.Identity.Name;
            return await _basketService.GetCheckoutAsync(orderDto, username);
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

        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetOrderItems(int orderId)
        {
            var order = _context.Orders
                .Where(o => o.AppUserId == _userManager.GetUserId(User))
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == orderId);
            return PartialView("_OrderItemsPartial", orderId);
        }

        [Authorize(Roles = "user")]
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
