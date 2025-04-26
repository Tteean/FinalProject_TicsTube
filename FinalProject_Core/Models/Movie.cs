using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static FinalProject_Core.Models.MovieComment;

namespace FinalProject_Core.Models
{
    public class Movie:Audit
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public string Video { get; set; }
        public string Image { get; set; }
        public List<MovieActor> MovieActors { get; set; }
        public List<MovieGenre> MovieGenres { get; set; }
        public List<MovieLanguage> MovieLanguages { get; set; }
        public List<MovieComment> MovieComments { get; set; }
        public List<MovieProduct> MovieProducts { get; set; }
        public int DirectorId { get; set; }
        public Director? Directors { get; set; }

    }
}
