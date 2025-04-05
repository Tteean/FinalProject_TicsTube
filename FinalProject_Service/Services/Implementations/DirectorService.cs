using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.ActorDtos;
using FinalProject_Service.Dto.DirectorDtos;
using FinalProject_Service.Dto.LanguageDtos;
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
    public class DirectorService : IDirectorService
    {
        private readonly TicsTubeDbContext _context;
        private readonly IMapper _mapper;

        public DirectorService(IMapper mapper, TicsTubeDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> CreateAsync(DirectorCreateDto directorCreateDto)
        {
            if (_context.Directors.Any(t => t.FullName == directorCreateDto.FullName))
            {
                throw new CustomException(400, "Name", "Director with this name already exists");
            }
            var director = _mapper.Map<Director>(directorCreateDto);
            if (directorCreateDto.File != null)
            {
                director.Image = directorCreateDto.File.SaveImage("uploads/directors");
            }
            await _context.Directors.AddAsync(director);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var existDirector = await _context.Directors
                .FirstOrDefaultAsync(a => a.Id == id);

            if (existDirector == null)
            {
                throw new CustomException(400, "Name", "Director with this name already exists");
            }
            DirectorDeleteDto directorDeleteDto = new DirectorDeleteDto();
            _mapper.Map(directorDeleteDto, existDirector);
            if (directorDeleteDto.File != null)
            {
                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "directors", existDirector.Image);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }
            _context.Directors.Remove(existDirector);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<DirectorReturnDto>> GetAllAsync()
        {
            var director = await _context.Directors.ToListAsync();
            return _mapper.Map<List<DirectorReturnDto>>(director);
        }

        public async Task<int> UpdateAsync(int id, DirectorUpdateDto directorUpdateDto)
        {
            if (_context.Directors.Any(g => g.FullName == directorUpdateDto.FullName && g.Id != id))
            {
                throw new CustomException(400, "Name", "Director with this name already exists");
            }
            var existDirector = await _context.Directors.FindAsync(id);
            if (existDirector == null)
            {
                throw new CustomException(404, "Director", "Director not found");
            }
            _mapper.Map(directorUpdateDto, existDirector);
            if (directorUpdateDto.File != null)
            {
                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "directors", existDirector.Image);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
                existDirector.Image = directorUpdateDto.File.SaveImage("uploads/directors");
            }
            return await _context.SaveChangesAsync();
        }
    }
}
