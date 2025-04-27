using FinalProject_Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Dto.TVShowDtos
{
    public class TVShowUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int DirectorId { get; set; }
        public Director? Directors { get; set; }
        public IFormFile? File { get; set; }
        public List<int> GenreIds { get; set; }
        public List<int> ActorId { get; set; }
        public List<int> LanguageId { get; set; }
    }

}
