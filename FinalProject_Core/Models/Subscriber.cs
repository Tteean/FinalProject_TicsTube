using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class Subscriber:BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
