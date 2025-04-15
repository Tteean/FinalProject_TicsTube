using FinalProject_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FinalProject_Core.Models.MovieComment;

namespace FinalProject_ViewModel.ViewModels
{
    public class MovieVm
    {
        public List<Movie> RelatedMovies { get; set; }
        public Movie Movie { get; set; }
        public bool HasCommentUser { get; set; }
        public int TotalComments { get; set; }
        public int AvgRate { get; set; }
        public MovieComment MovieComment { get; set; }
    }
}
