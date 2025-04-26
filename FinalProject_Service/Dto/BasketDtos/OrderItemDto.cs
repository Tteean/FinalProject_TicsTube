using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Dto.BasketDtos
{
    public class OrderItemDto
    {
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal PricePerItem { get; set; }
    }
}
