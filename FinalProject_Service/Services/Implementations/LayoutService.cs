using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.BasketDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Implementations
{
    public class LayoutService
    {
        private readonly TicsTubeDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpContext _httpContext;

        public LayoutService(TicsTubeDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _httpContext = _contextAccessor.HttpContext;

        }

        public List<BasketItemDto> GetUserBasketItems()
        {
            List<BasketItemDto> list;
            var basket = _httpContext.Request.Cookies["basket"];
            if (basket != null)
            {
                list = JsonSerializer.Deserialize<List<BasketItemDto>>(basket);
            }
            else
            {
                list = new();
            }
            var user = _context.Users
                .Include(u => u.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefault(u => u.UserName == _httpContext.User.Identity.Name);
            if (user != null)
            {
                foreach (var dbProduct in user.BasketItems)
                {
                    if (!list.Any(b => b.Id == dbProduct.ProductId))
                    {
                        BasketItemDto basketItemDto = new();
                        basketItemDto.Id = dbProduct.ProductId;
                        basketItemDto.Name = dbProduct.Product.Name;
                        basketItemDto.Image = dbProduct.Product.Image;
                        basketItemDto.Price = dbProduct.Product.CostPrice;
                        basketItemDto.Count = dbProduct.Count;
                        list.Add(basketItemDto);
                    }
                }
            }
            foreach (var item in list)
            {
                var existProduct = _context.Products.FirstOrDefault(p => p.Id == item.Id);
                item.Name = existProduct.Name;
                item.Image = existProduct.Image;
                item.Price = existProduct.CostPrice;
            }


            _httpContext.Response.Cookies.Append("basket", JsonSerializer.Serialize(list));

            return list;
        }
        public Dictionary<string, string> GetAllSettings()
        {
            return _context.Settings.ToDictionary(s => s.Key, s => s.Value);
        }

    }
}
