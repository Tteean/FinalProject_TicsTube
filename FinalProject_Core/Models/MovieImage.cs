using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class MovieImage:Audit
    {
        public string Name { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public bool Status { get; set; }
    }
}
