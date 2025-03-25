
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class AppUser:IdentityUser
    {
        public string? FullName { get; set; }
        public bool IsSubscribed { get; set; }
    }
}
