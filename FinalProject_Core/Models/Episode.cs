using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class Episode:Audit
    {
        public int EpisodeNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string Video { get; set; }
        public string Image { get; set; }
        public int SeasonId { get; set; }
        public Season? Seasons { get; set; }
    }
}
