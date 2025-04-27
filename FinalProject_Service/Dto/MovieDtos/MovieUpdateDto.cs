using FinalProject_Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Dto.MovieDtos
{
    public class MovieUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public double Rating { get; set; }
        public int DirectorId { get; set; }
        public Director? Directors { get; set; }
        public IFormFile? File { get; set; }
        public IFormFile? Film { get; set; }
        public List<int> GenreIds { get; set; }
        public List<int> ActorId { get; set; }
        public List<int> LanguageId { get; set; }
    }

}
