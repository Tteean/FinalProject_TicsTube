using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class Movie:Audit
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public double Rating { get; set; }
        public List<MovieImage> MovieImages { get; set; }
        public List<MovieGenre> MovieGenres { get; set; }
        public List<int> GenreIds { get; set; }

    }
}
