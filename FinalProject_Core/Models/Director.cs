using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class Director:Audit
    {
        public string FullName { get; set; }
        public string? Image { get; set; }
        public List<Movie> Movies { get; set; }
        public List<TVShow> TVShows { get; set; }
    }
}
