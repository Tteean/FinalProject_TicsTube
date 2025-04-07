using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.ActorDtos;
using FinalProject_Service.Dto.MovieDtos;
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
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

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

        public async Task<int> CreateAsync(MovieCreateDto movieCreateDto)
        {
            if (!_context.Directors.Any(d => d.Id == movieCreateDto.DirectorId))
            {
                throw new CustomException(404, "Director", "Director not found");
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
            foreach (var actorId in movieCreateDto.ActorId)
            {
                if (!_context.Actors.Any(t => t.Id == actorId))
                {
                    throw new CustomException(404, "Actor", "Actor not found");
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
            foreach (var actorId in movieCreateDto.ActorId)
            {
                movie.MovieActors.Add(new MovieActor { ActorId = actorId });
            }
            if (movieCreateDto.Film != null)
            {
                movie.Video = movieCreateDto.Film.SaveImage("uploads/movies");
            }
            if (movieCreateDto.File != null)
            {
                movie.Image = movieCreateDto.File.SaveImage("uploads/movies");
            }
            _context.Movies.Add(movie);
            return _context.SaveChanges();
        }

        public async Task<int> DeleteAsync(int id)
        {

            var existMovie = await _context.Movies
                .Include(p => p.MovieActors)
                .Include(p => p.MovieGenres)
                .Include(p => p.MovieLanguages)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existMovie == null)
            {
                throw new CustomException(404, "Movie", "Movie not found");
            }
            MovieDeleteDto movieDeleteDto = new MovieDeleteDto();
            _mapper.Map(movieDeleteDto, existMovie);
            if (movieDeleteDto.File != null)
            {
                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "movies", existMovie.Image);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }
            if (movieDeleteDto.Film != null)
            {
                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "movies", existMovie.Video);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }
            _context.MovieActors.RemoveRange(existMovie.MovieActors);
            _context.MovieGenres.RemoveRange(existMovie.MovieGenres);
            _context.MovieLanguages.RemoveRange(existMovie.MovieLanguages);
            _context.Movies.Remove(existMovie);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<MovieReturnDto>> GetAllAsync()
        {
            var movies = await _context.Movies
                .Include(p => p.MovieActors)
                .Include(p => p.MovieGenres)
                .Include(p => p.MovieLanguages).ToListAsync();
            return _mapper.Map<List<MovieReturnDto>>(movies);
        }

        public async Task<int> UpdateAsync(int id, MovieUpdateDto movieUpdateDto)
        {
            if (!_context.Directors.Any(d => d.Id == movieUpdateDto.DirectorId))
            {
                throw new CustomException(404, "Director", "Director not found");
            }
            var existMovie = await _context.Movies
                .Include(p => p.MovieActors)
                .Include(p => p.MovieGenres)
                .Include(p => p.MovieLanguages)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (existMovie == null)
            {
                throw new CustomException(404, "Movie", "Movie not found");
            }
            _mapper.Map(movieUpdateDto, existMovie);
            foreach (var movieActor in existMovie.MovieActors.ToList())
            {
                _context.MovieActors.Remove(movieActor);
                existMovie.MovieActors.Remove(movieActor);
            }
            foreach (var actorId in movieUpdateDto.ActorId ?? new())
            {
                if (!_context.Actors.Any(t => t.Id == actorId))
                {
                    throw new CustomException(404, "Actor", "Actor not found");
                }
                existMovie.MovieActors.Add(new MovieActor { ActorId = actorId });
            }
            foreach (var movieGenre in existMovie.MovieGenres.ToList())
            {
                _context.MovieGenres.Remove(movieGenre);
                existMovie.MovieGenres.Remove(movieGenre);
            }
            foreach (var genreId in movieUpdateDto.GenreIds ?? new())
            {
                if (!_context.Genres.Any(t => t.Id == genreId))
                {
                    throw new CustomException(404, "Genre", "Genre not found");
                }
                existMovie.MovieGenres.Add(new MovieGenre { GenreId = genreId });
            }
            foreach (var movieLanguage in existMovie.MovieLanguages.ToList())
            {
                _context.MovieLanguages.Remove(movieLanguage);
                existMovie.MovieLanguages.Remove(movieLanguage);
            }
            foreach (var languageId in movieUpdateDto.LanguageId ?? new())
            {
                if (!_context.Languages.Any(t => t.Id == languageId))
                {
                    throw new CustomException(404, "Language", "Language not found");
                }
                existMovie.MovieLanguages.Add(new MovieLanguage { LanguageId = languageId });
            }
            if (movieUpdateDto.File != null)
            {
                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "movies", existMovie.Image);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
                existMovie.Image = movieUpdateDto.File.SaveImage("uploads/movies");
            }
            if (movieUpdateDto.Film != null)
            {
                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "movies", existMovie.Video);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
                existMovie.Video = movieUpdateDto.Film.SaveImage("uploads/movies");
            }
            return await _context.SaveChangesAsync();
        }
    }
}
