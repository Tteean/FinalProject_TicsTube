using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.EpisodeDtos;
using FinalProject_Service.Dto.SeasonDtos;
using FinalProject_Service.Exceptions;
using FinalProject_Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Implementations
{
    public class EpisodeService:IEpisodeService
    {
        private readonly TicsTubeDbContext _context;
        private readonly IMapper _mapper;

        public EpisodeService(IMapper mapper, TicsTubeDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<int> CreateAsync(EpisodeCreateDto episodeCreateDto)
        {
            if (_context.Episodes.Any(s => s.EpisodeNumber == episodeCreateDto.EpisodeNumber && s.SeasonId == episodeCreateDto.SeasonId))
            {
                throw new CustomException(400, "Name", "Episode with this number already exists");
            }
            var episode = _mapper.Map<Episode>(episodeCreateDto);
            await _context.Episodes.AddAsync(episode);
            return await _context.SaveChangesAsync();
        }
    }
}
