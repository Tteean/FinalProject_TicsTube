using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class Language:Audit
    {
        public string Name { get; set; }
        public List<MovieLanguage> MovieLanguages { get; set; }
        public List<TVShowLanguage> TVShowLanguages { get; set; }
    }
}
