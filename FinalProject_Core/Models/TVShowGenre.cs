using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class TVShowGenre:BaseEntity
    {
        public int TVShowId { get; set; }
        public TVShow? TVShow { get; set; }
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
