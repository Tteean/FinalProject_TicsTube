using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Dto.BasketDtos
{
    public class CheckoutItemDto
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal TotalItemPrice { get; set; }
    }

}
