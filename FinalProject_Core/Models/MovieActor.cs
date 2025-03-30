using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class MovieActor:BaseEntity
    {
        public int ActorId { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public Actor? Actor { get; set; }
    }
}
