using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.EpisodeDtos;
using FinalProject_Service.Dto.MovieDtos;
using FinalProject_Service.Dto.SeasonDtos;
using FinalProject_Service.Exceptions;
using FinalProject_Service.Extentions;
using FinalProject_Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
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
            if (episodeCreateDto.File != null)
            {
                episode.Image = episodeCreateDto.File.SaveImage("uploads/episodes");
            }
            else
            {
                throw new CustomException(400, "Image", "Image is required");
            }

            if (episodeCreateDto.Film != null)
            {
                episode.Video = episodeCreateDto.Film.SaveImage("uploads/episodes");
            }
            else
            {
                throw new CustomException(400, "Video", "Video is required");
            }
            await _context.Episodes.AddAsync(episode);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var existEpisode = await _context.Episodes
                .FirstOrDefaultAsync(a => a.Id == id);

            if (existEpisode == null)
            {
                throw new CustomException(400, "Name", "Episode with this number already exists");
            }
            EpisodeDeleteDto episodeDeleteDto = new EpisodeDeleteDto();
            _mapper.Map(episodeDeleteDto, existEpisode);
            if (episodeDeleteDto.File != null)
            {
                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "episodes", existEpisode.Image);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }
            if (episodeDeleteDto.Film != null)
            {
                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "episodes", existEpisode.Video);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }
            _context.Episodes.Remove(existEpisode);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<EpisodeReturnDto>> GetAllAsync()
        {
            var episodes = await _context.Episodes.ToListAsync();
            return _mapper.Map<List<EpisodeReturnDto>>(episodes);
        }

        public async Task<int> UpdateAsync(int id, EpisodeUpdateDto episodeUpdateDto)
        {
            if (_context.Seasons.Any(g => g.SeasonNumber == episodeUpdateDto.EpisodeNumber && g.Id != id))
            {
                throw new CustomException(400, "Name", "Episode with this name already exists");
            }
            var existEpisode = await _context.Episodes.FindAsync(id);
            if (existEpisode == null)
            {
                throw new CustomException(404, "Episode", "Episode not found");
            }
            _mapper.Map(episodeUpdateDto, existEpisode);
            if (episodeUpdateDto.File != null)
            {
                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "episodes", existEpisode.Image);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
                existEpisode.Image = episodeUpdateDto.File.SaveImage("uploads/episodes");
            }
            if (episodeUpdateDto.Film != null)
            {
                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "episodes", existEpisode.Video);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
                existEpisode.Video = episodeUpdateDto.Film.SaveImage("uploads/episodes");
            }
            return await _context.SaveChangesAsync();
        }
    }
}
