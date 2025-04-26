using FinalProject_Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class Order:Audit
    {
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? ZipCode { get; set; }
        public decimal? TotalPrice { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
