using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class TVShowActor:BaseEntity
    {
        public int ActorId { get; set; }
        public int TVShowId { get; set; }
        public TVShow? TVShow { get; set; }
        public Actor? Actor { get; set; }
    }
}
