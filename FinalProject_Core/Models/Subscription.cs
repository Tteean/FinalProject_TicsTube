using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class Subscription:BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string Plan { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
