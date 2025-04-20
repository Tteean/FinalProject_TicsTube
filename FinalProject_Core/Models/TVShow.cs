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
        public string Image { get; set; }
        public List<TVShowActor> TVShowActors { get; set; }
        public List<TVShowGenre> TVShowGenres { get; set; }
        public List<TVShowLanguage> TVShowLanguages { get; set; }
        public List<Season> Seasons { get; set; }
        public int DirectorId { get; set; }
        public Director? Directors { get; set; }
        public TVShow()
        {
            Seasons = new List<Season>();
        }
    }
}
