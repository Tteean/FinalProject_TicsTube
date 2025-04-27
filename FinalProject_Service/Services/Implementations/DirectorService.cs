using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.ActorDtos;
using FinalProject_Service.Dto.DirectorDtos;
using FinalProject_Service.Dto.LanguageDtos;
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

namespace FinalProject_Service.Services.Implementations
{
    public class DirectorService : IDirectorService
    {
        private readonly TicsTubeDbContext _context;
        private readonly IMapper _mapper; 
        private readonly PhotoService _photoService;

        public DirectorService(IMapper mapper, TicsTubeDbContext context, PhotoService photoService)
        {
            _mapper = mapper;
            _context = context;
            _photoService = photoService;
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
                director.Image = await _photoService.UploadImageAsync(directorCreateDto.File);

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
            if (!string.IsNullOrEmpty(existDirector.Image))
            {
                await _photoService.DeleteAsync(existDirector.Image);
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
                if (!string.IsNullOrEmpty(existDirector.Image))
                {
                    await _photoService.DeleteAsync(existDirector.Image);
                }
                existDirector.Image = await _photoService.UploadImageAsync(directorUpdateDto.File);
            }

            return await _context.SaveChangesAsync();
        }
    }
}
