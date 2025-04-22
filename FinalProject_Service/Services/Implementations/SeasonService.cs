using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.GenreDtos;
using FinalProject_Service.Dto.SeasonDtos;
using FinalProject_Service.Dto.TVShowDtos;
using FinalProject_Service.Exceptions;
using FinalProject_Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> DeleteAsync(int id)
        {
            var existSeason = await _context.Seasons
                .FirstOrDefaultAsync(a => a.Id == id);

            if (existSeason == null)
            {
                throw new CustomException(400, "Name", "Season with this number already exists");
            }
            SeasonDeleteDto seasonDeleteDto = new SeasonDeleteDto();
            _mapper.Map(seasonDeleteDto, existSeason);
            _context.Seasons.Remove(existSeason);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<SeasonReturnDto>> GetAllAsync()
        {
            var seasons = await _context.Seasons.ToListAsync();
            return _mapper.Map<List<SeasonReturnDto>>(seasons);
        }

        public async Task<int> UpdateAsync(int id, SeasonUpdateDto seasonUpdateDto)
        {
            if (_context.Seasons.Any(g => g.SeasonNumber == seasonUpdateDto.SeasonNumber && g.Id != id))
            {
                throw new CustomException(400, "Name", "Genre with this name already exists");
            }
            var existSeason = await _context.Seasons.FindAsync(id);
            if (existSeason == null)
            {
                throw new CustomException(404, "Season", "Season not found");
            }
            _mapper.Map(seasonUpdateDto, existSeason);
            return await _context.SaveChangesAsync();
        }
    }
}
