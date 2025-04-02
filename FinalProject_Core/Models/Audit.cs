using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class Audit:BaseEntity
    {
        public DateTime CreationDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime DeleteDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
