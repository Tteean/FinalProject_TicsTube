using FinalProject_ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Dto.BasketDtos
{
    public class CheckoutDto
    {
        public OrderDto OrderDto { get; set; }
        public List<CheckoutItemDto> CheckoutItemDtos{ get; set; }
    }
}
