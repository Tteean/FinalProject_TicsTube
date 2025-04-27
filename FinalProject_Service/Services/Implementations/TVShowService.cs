using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.ActorDtos;
using FinalProject_Service.Dto.MovieDtos;
using FinalProject_Service.Dto.TVShowDtos;
using FinalProject_Service.Exceptions;
using FinalProject_Service.Extentions;
using FinalProject_Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Implementations
{
    public class TVShowService : ITVShowService
    {
        private readonly TicsTubeDbContext _context;
        private readonly IMapper _mapper;
        private readonly PhotoService _photoService;

        public TVShowService(IMapper mapper, TicsTubeDbContext context, PhotoService photoService)
        {
            _mapper = mapper;
            _context = context;
            _photoService = photoService;
        }

        public async Task<int> CreateAsync(TVShowCreateDto tVShowCreateDto)
        {
            if (!_context.Directors.Any(d => d.Id == tVShowCreateDto.DirectorId))
            {
                throw new CustomException(404, "Director", "Director not found");
            }

            foreach (var genreId in tVShowCreateDto.GenreIds)
            {
                if (!_context.Genres.Any(t => t.Id == genreId))
                {
                    throw new CustomException(404, "Genre", "Genre not found");
                }
            }

            foreach (var languageId in tVShowCreateDto.LanguageId)
            {
                if (!_context.Languages.Any(s => s.Id == languageId))
                {
                    throw new CustomException(404, "Language", "Language not found");
                }
            }
            foreach (var actorId in tVShowCreateDto.ActorId)
            {
                if (!_context.Actors.Any(t => t.Id == actorId))
                {
                    throw new CustomException(404, "Actor", "Actor not found");
                }
            }
            var tvShow = _mapper.Map<TVShow>(tVShowCreateDto);
            foreach (var genreId in tVShowCreateDto.GenreIds)
            {
                tvShow.TVShowGenres.Add(new TVShowGenre { GenreId = genreId });
            }
            foreach (var languageId in tVShowCreateDto.LanguageId)
            {
                tvShow.TVShowLanguages.Add(new TVShowLanguage { LanguageId = languageId });
            }
            foreach (var actorId in tVShowCreateDto.ActorId)
            {
                tvShow.TVShowActors.Add(new TVShowActor { ActorId = actorId });
            }
            if (tVShowCreateDto.File != null)
            {
                tvShow.Image = await _photoService.UploadImageAsync(tVShowCreateDto.File);

            }
            _context.TVShows.Add(tvShow);
            return _context.SaveChanges();
        }   

        public async Task<int> DeleteAsync(int id)
        {
            var existShow = await _context.TVShows
                .Include(p => p.TVShowActors)
                .Include(p => p.TVShowGenres)
                .Include(p => p.TVShowLanguages)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existShow == null)
            {
                throw new CustomException(404, "Movie", "Movie not found");
            }
            TVShowDeleteDto tVShowDeleteDto = new TVShowDeleteDto();
            _mapper.Map(tVShowDeleteDto, existShow);
            if (!string.IsNullOrEmpty(existShow.Image))
            {
                await _photoService.DeleteAsync(existShow.Image);
            }
            _context.TVShowActors.RemoveRange(existShow.TVShowActors);
            _context.TVShowGenres.RemoveRange(existShow.TVShowGenres);
            _context.TVShowLanguages.RemoveRange(existShow.TVShowLanguages);
            _context.Seasons.RemoveRange(existShow.Seasons);  
            _context.TVShows.Remove(existShow);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<TVShowReturnDto>> GetAllAsync()
        {
            var shows = await _context.TVShows
                .Include(p => p.TVShowActors)
                .Include(p => p.TVShowGenres)
                .Include(p => p.TVShowLanguages).ToListAsync();
            return _mapper.Map<List<TVShowReturnDto>>(shows);
        }
        public async Task<int> UpdateAsync(int id, TVShowUpdateDto tVShowUpdateDto)
        {
            if (!_context.Directors.Any(d => d.Id == tVShowUpdateDto.DirectorId))
            {
                throw new CustomException(404, "Director", "Director not found");
            }
            var existShow = await _context.TVShows
                .Include(p => p.TVShowActors)
                .Include(p => p.TVShowGenres)
                .Include(p => p.TVShowLanguages)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (existShow == null)
            {
                throw new CustomException(404, "TVShow", "TV Show not found");
            }
            _mapper.Map(tVShowUpdateDto, existShow);
            foreach (var showActor in existShow.TVShowActors.ToList())
            {
                _context.TVShowActors.Remove(showActor);
                existShow.TVShowActors.Remove(showActor);
            }
            foreach (var actorId in tVShowUpdateDto.ActorId ?? new())
            {
                if (!_context.Actors.Any(t => t.Id == actorId))
                {
                    throw new CustomException(404, "Actor", "Actor not found");
                }
                existShow.TVShowActors.Add(new TVShowActor { ActorId = actorId });
            }
            foreach (var showGenre in existShow.TVShowGenres.ToList())
            {
                _context.TVShowGenres.Remove(showGenre);
                existShow.TVShowGenres.Remove(showGenre);
            }
            foreach (var genreId in tVShowUpdateDto.GenreIds ?? new())
            {
                if (!_context.Genres.Any(t => t.Id == genreId))
                {
                    throw new CustomException(404, "Genre", "Genre not found");
                }
                existShow.TVShowGenres.Add(new TVShowGenre { GenreId = genreId });
            }
            foreach (var showLanguage in existShow.TVShowLanguages.ToList())
            {
                _context.TVShowLanguages.Remove(showLanguage);
                existShow.TVShowLanguages.Remove(showLanguage);
            }
            foreach (var languageId in tVShowUpdateDto.LanguageId ?? new())
            {
                if (!_context.Languages.Any(t => t.Id == languageId))
                {
                    throw new CustomException(404, "Language", "Language not found");
                }
                existShow.TVShowLanguages.Add(new TVShowLanguage { LanguageId = languageId });
            }
            if (tVShowUpdateDto.File != null)
            {
                if (!string.IsNullOrEmpty(existShow.Image))
                {
                    await _photoService.DeleteAsync(existShow.Image);
                }
                existShow.Image = await _photoService.UploadImageAsync(tVShowUpdateDto.File);
            }

            return await _context.SaveChangesAsync();
        }
    }
}
