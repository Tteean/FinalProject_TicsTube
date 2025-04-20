using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.GenreDtos;
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
    public class SeasonService:ISeasonService
    {
        private readonly TicsTubeDbContext _context;
        private readonly IMapper _mapper;

        public SeasonService(IMapper mapper, TicsTubeDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<int> CreateAsync(SeasonCreateDto seasonCreateDto)
        {
            if (_context.Seasons.Any(s => s.SeasonNumber == seasonCreateDto.SeasonNumber && s.TVShowId==seasonCreateDto.TVShowId))
            {
                throw new CustomException(400, "Name", "Season with this number already exists");
            }
            var season = _mapper.Map<Season>(seasonCreateDto);
            await _context.Seasons.AddAsync(season);
            return await _context.SaveChangesAsync();
        }
    }
}
