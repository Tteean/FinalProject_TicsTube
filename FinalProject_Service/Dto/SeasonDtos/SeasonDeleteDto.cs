using FinalProject_Service.Dto.EpisodeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Dto.SeasonDtos
{
    public class SeasonDeleteDto
    {
        public int Id { get; set; }
        public int TVShowId { get; set; }
        public int SeasonNumber { get; set; }
        public List<EpisodeCreateDto>? Episodes { get; set; }
    }

}
