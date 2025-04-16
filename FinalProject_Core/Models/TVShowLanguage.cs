using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class TVShowLanguage:BaseEntity
    {
        public int LanguageId { get; set; }
        public int TVShowId { get; set; }
        public TVShow? TVShow { get; set; }
        public Language? Language { get; set; }
    }
}
