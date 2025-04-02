using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.ActorDtos;
using FinalProject_Service.Dto.GenreDtos;
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
    public class GenreService : IGenreService
    {

        private readonly TicsTubeDbContext _context;
        private readonly IMapper _mapper;

        public GenreService(IMapper mapper, TicsTubeDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> CreateAsync(CreateGenreDto createGenreDto)
        {
            if (_context.Genres.Any(t => t.Name == createGenreDto.Name))
            {
                throw new CustomException(400, "Name", "Genre with this name already exists");
            }
            var genre = _mapper.Map<Genre>(createGenreDto);
            await _context.Genres.AddAsync(genre);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var existGenre = await _context.Genres
                .FirstOrDefaultAsync(a => a.Id == id);

            if (existGenre == null)
            {
                throw new CustomException(400, "Name", "Genre with this name already exists");
            }
            DeleteGenreDto deleteGenreDto = new DeleteGenreDto();
            _mapper.Map(deleteGenreDto, existGenre);
            _context.Genres.Remove(existGenre);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<GenreReturnDto>> GetAllAsync()
        {

            var genres = await _context.Genres.ToListAsync();
            return _mapper.Map<List<GenreReturnDto>>(genres);
        }

        public async Task<int> UpdateAsync(int id, UpdateGenreDto updateGenreDto)
        {
            if (_context.Genres.Any(g => g.Name == updateGenreDto.Name && g.Id != id))
            {
                throw new CustomException(400, "Name", "Genre with this name already exists");
            }
            var existGenre = await _context.Genres.FindAsync(id);
            if (existGenre == null)
            {
                throw new CustomException(404, "Genre", "Genre not found");
            }
            _mapper.Map(updateGenreDto, existGenre);
            return await _context.SaveChangesAsync();
        }
    }
}
