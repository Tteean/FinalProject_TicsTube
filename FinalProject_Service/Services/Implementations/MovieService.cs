using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.MovieDtos;
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
    public class MovieService:IMovieService
    {
        private readonly TicsTubeDbContext _context;
        private readonly IMapper _mapper;

        public MovieService(IMapper mapper, TicsTubeDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> CreateActorAsync(MovieCreateDto movieCreateDto)
        {
            if (!_context.Movies.Any(c => c.Title == movieCreateDto.Title))
            {
                throw new CustomException(400, "Name", "Movie with this name already exists");
            }

            foreach (var genreId in movieCreateDto.GenreIds)
            {
                if (!_context.Genres.Any(t => t.Id == genreId))
                {
                    throw new CustomException(404, "Genre", "Genre not found");
                }
            }

            foreach (var languageId in movieCreateDto.LanguageId)
            {
                if (!_context.Languages.Any(s => s.Id == languageId))
                {
                    throw new CustomException(404, "Language", "Language not found");
                }
            }
            var movie = _mapper.Map<Movie>(movieCreateDto);
            foreach (var genreId in movieCreateDto.GenreIds)
            {
                movie.MovieGenres.Add(new MovieGenre { GenreId = genreId });
            }

            foreach (var languageId in movieCreateDto.LanguageId)
            {
                movie.MovieLanguages.Add(new MovieLanguage { LanguageId = languageId });
            }
            if (movieCreateDto.File is { Length: > 0 })
            {
                for (int i = 0; i < movieCreateDto.File.Length; i++)
                {
                    var file = movieCreateDto.File[i];
                    string name = file.SaveImage("uploads/movies");

                    movie.MovieImages.Add(new MovieImage
                    {
                        Name = name,
                        Status = i == 0
                    });
                }
            }
            _context.Movies.Add(movie);
            return _context.SaveChanges();
        }

        public async Task<int> DeleteActorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MovieReturnDto>> GetActorAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateActorAsync(int id, MovieUpdateDto movieUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
