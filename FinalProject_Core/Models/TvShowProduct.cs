using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class TvShowProduct
    {
        public int ProductId { get; set; }
        public int TvShowId { get; set; }
        public TVShow? TVShow { get; set; }
        public Product? Product { get; set; }
    }
}
