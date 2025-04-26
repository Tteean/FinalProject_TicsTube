﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Dto.BasketDtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
