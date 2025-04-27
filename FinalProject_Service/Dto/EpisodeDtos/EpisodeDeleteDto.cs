using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Dto.EpisodeDtos
{
    public class EpisodeDeleteDto
    {
        public int Id { get; set; }
        public int? TVShowId { get; set; }
        public int? SeasonId { get; set; }
        public int EpisodeNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public IFormFile? File { get; set; }
        public IFormFile? Film { get; set; }
    }

}
