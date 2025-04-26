using FinalProject_Core.Models;
using FinalProject_Service.Dto.BasketDtos;
using FinalProject_ViewModel.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Interfaces
{
    public interface IBasketService
    {
        Task<List<BasketItemDto>> GetBasketAsync();
        Task<List<BasketItemDto>> AddToBasketAsync(int id);
        Task<List<BasketItemDto>> RemoveFromBasketAsync(int id);
        Task<CheckoutDto> GetCheckoutAsync();
        Task<int> SubmitOrderAsync(OrderCreateDto orderDto);
    }
}
