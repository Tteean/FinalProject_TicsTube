using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class TVShow:Audit
    {
        public int Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string Video { get; set; }
        public string Image { get; set; }
        public List<MovieActor> MovieActors { get; set; }
        public List<MovieGenre> MovieGenres { get; set; }
        public List<MovieLanguage> MovieLanguages { get; set; }
        public List<MovieComment> MovieComments { get; set; }
        public int DirectorId { get; set; }
        public Director? Directors { get; set; }
    }
}
