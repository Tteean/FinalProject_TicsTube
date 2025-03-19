using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class Genre:Audit
    {
        public string Name { get; set; }
        public List<MovieGenre> MovieGenres { get; set; }
    }
}
