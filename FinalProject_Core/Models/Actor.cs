using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class Actor:Audit
    {
        public string Fullname { get; set; }
        public string Image { get; set; }
        public List<MovieActor> MovieActors { get; set; }


    }
}
