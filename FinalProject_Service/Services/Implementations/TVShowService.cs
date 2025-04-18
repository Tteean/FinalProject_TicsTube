using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.MovieDtos;
using FinalProject_Service.Dto.TVShowDtos;
using FinalProject_Service.Exceptions;
using FinalProject_Service.Extentions;
using FinalProject_Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Implementations
{
    public class TVShowService : ITVShowService
    {
        private readonly TicsTubeDbContext _context;
        private readonly IMapper _mapper;

        public TVShowService(IMapper mapper, TicsTubeDbContext context)
        {
            _mapper = mapper;
            _context = context;
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
                tvShow.Image = tVShowCreateDto.File.SaveImage("uploads/TVShow");
            }
            _context.TVShows.Add(tvShow);
            return _context.SaveChanges();
        }   

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TVShowReturnDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(int id, TVShowUpdateDto tVShowUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
