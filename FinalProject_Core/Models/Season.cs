using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class Season:BaseEntity
    {
        public int SeasonNumber { get; set; }
        public int TVShowId { get; set; }
        public TVShow? TVShow { get; set; }
        public List<Episode> Episodes { get; set; }
        public Season() 
        {
            Episodes = new List<Episode>();
        }
    }
}
