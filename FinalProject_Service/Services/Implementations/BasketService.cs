using AutoMapper;
using FinalProject_Core.Enum;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.BasketDtos;
using FinalProject_Service.Exceptions;
using FinalProject_Service.Services.Interfaces;
using FinalProject_ViewModel.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TicsTubeDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public BasketService(TicsTubeDbContext context, UserManager<AppUser> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<BasketItemDto>> GetBasketAsync()
        {
            if (IsUserAuthenticated())
            {
                var user = await GetCurrentUserAsync();
                return user.BasketItems
                    .Select(b => new BasketItemDto
                    {
                        Id = b.ProductId,
                        Name = b.Product.Name,
                        Price = b.Product.CostPrice,
                        Image = b.Product.Image,
                        Count = b.Count
                    })
                    .ToList();
            }

            var basketJson = _httpContextAccessor.HttpContext.Request.Cookies["basket"];
            var basketItems = basketJson == null
                ? new List<BasketItemDto>()
                : JsonSerializer.Deserialize<List<BasketItemDto>>(basketJson);
            return basketItems;
        }

        public async Task<List<BasketItemDto>> AddToBasketAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) throw new CustomException(404, "Product", "Product not found");

            if (IsUserAuthenticated())
            {
                var user = await GetCurrentUserAsync();
                var userBasketItem = user.BasketItems.FirstOrDefault(b => b.ProductId == id);
                if (userBasketItem != null)
                {
                    userBasketItem.Count++;
                }
                else
                {
                    user.BasketItems.Add(new BasketItem { ProductId = id, Count = 1 });
                }

                await _context.SaveChangesAsync();
            }
            else
            {
                var basketItems = await GetBasketAsync();
                var existingItem = basketItems.FirstOrDefault(b => b.Id == id);

                if (existingItem != null)
                {
                    existingItem.Count++;
                }
                else
                {
                    var basketItemDto = new BasketItemDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.CostPrice,
                        Image = product.Image,
                        Count = 1
                    };
                    basketItems.Add(basketItemDto);
                }

                UpdateBasketCookie(basketItems);
            }

            return await GetBasketAsync();
        }

        public async Task<List<BasketItemDto>> RemoveFromBasketAsync(int id)
        {
            var basketItems = await GetBasketAsync();
            var item = basketItems.FirstOrDefault(b => b.Id == id);

            if (item != null)
            {
                if (item.Count > 1)
                    item.Count--;
                else
                    basketItems.Remove(item);
            }

            if (IsUserAuthenticated())
            {
                var user = await GetCurrentUserAsync();
                var userItem = user.BasketItems.FirstOrDefault(b => b.ProductId == id);
                if (userItem != null)
                {
                    if (userItem.Count > 1)
                        userItem.Count--;
                    else
                    _context.BasketItems.Remove(userItem);
                    await _context.SaveChangesAsync();
                }
            }

            UpdateBasketCookie(basketItems);
            return basketItems;
        }

        public async Task<IActionResult> GetCheckoutAsync(OrderDto orderDto, string username)
        {
            var user = _userManager.Users
            .Include(u => u.BasketItems)
            .ThenInclude(b => b.Product)
            .FirstOrDefault(u => u.UserName == username);

            if (user == null)
                throw new CustomException(400, "User", "User not found"); ;

            if (orderDto == null)
            {
                var checkoutDto = new CheckoutDto
                {
                    CheckoutItemDtos = user.BasketItems.Select(b => new CheckoutItemDto
                    {
                        Name = b.Product.Name,
                        TotalItemPrice = b.Product.CostPrice * b.Count,
                        Count = b.Count
                    }).ToList(),
                    OrderDto = new OrderDto()
                };

                return new ViewResult
                {
                    ViewName = "Checkout",
                    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                    {
                        Model = checkoutDto
                    }
                };
            }

            var order = new Order
            {
                AppUserId = user.Id,
                Country = orderDto.Country,
                ZipCode = orderDto.ZipCode,
                City = orderDto.City,
                Address = orderDto.Address,
                TotalPrice = user.BasketItems.Sum(b =>
                    b.Product.CostPrice * b.Count),
                OrderStatus = OrderStatus.Pending,
                OrderItems = user.BasketItems.Select(b => new OrderItem
                {
                    ProductId = b.ProductId,
                    Count = b.Count
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.BasketItems.RemoveRange(user.BasketItems);
            _context.SaveChanges();

            _httpContextAccessor.HttpContext.Response.Cookies.Delete("basket");

            return new RedirectToActionResult("Profile", "Account", new { tab = "orders" });
        }


        public async Task<int> SubmitOrderAsync(OrderCreateDto orderDto)
        {
            var user = await GetCurrentUserWithBasketAsync();
            if (!user.BasketItems.Any()) throw new CustomException(400, "Basket", "Basket is empty");

            var order = _mapper.Map<Order>(orderDto);
            order.AppUserId = user.Id;
            order.OrderStatus = OrderStatus.Pending;
            order.TotalPrice = user.BasketItems.Sum(b => (b.Product.CostPrice * b.Count));
            order.OrderItems = user.BasketItems.Select(b => new OrderItem { ProductId = b.ProductId, Count = b.Count }).ToList();

            _context.Orders.Add(order);
            _context.BasketItems.RemoveRange(user.BasketItems);
            await _context.SaveChangesAsync();

            _httpContextAccessor.HttpContext.Response.Cookies.Delete("basket");
            return order.Id;
        }

        private void UpdateBasketCookie(List<BasketItemDto> basketItems)
        {
            var basketJson = JsonSerializer.Serialize(basketItems);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", basketJson);
        }

        private bool IsUserAuthenticated() =>
            _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated &&
            _httpContextAccessor.HttpContext.User.IsInRole("user");

        private async Task<AppUser> GetCurrentUserAsync() =>
    await _userManager.Users
        .Include(u => u.BasketItems)
        .ThenInclude(b => b.Product)
        .FirstOrDefaultAsync(u => u.UserName == _httpContextAccessor.HttpContext.User.Identity.Name);

        private async Task<AppUser> GetCurrentUserWithBasketAsync() =>
            await _userManager.Users.Include(u => u.BasketItems).ThenInclude(b => b.Product)
                .FirstOrDefaultAsync(u => u.UserName == _httpContextAccessor.HttpContext.User.Identity.Name);
    }
}

