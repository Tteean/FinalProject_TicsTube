using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.GenreDtos;
using FinalProject_Service.Dto.LanguageDtos;
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
    public class LanguageService : ILanguageService
    {
        private readonly TicsTubeDbContext _context;
        private readonly IMapper _mapper;

        public LanguageService(IMapper mapper, TicsTubeDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> CreateAsync(LanguageCreateDto languageCreateDto)
        {
            if (_context.Genres.Any(t => t.Name == languageCreateDto.Name))
            {
                throw new CustomException(400, "Name", "Language with this name already exists");
            }
            var language = _mapper.Map<Language>(languageCreateDto);
            await _context.Languages.AddAsync(language);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var existLanguage = await _context.Languages
                .FirstOrDefaultAsync(a => a.Id == id);

            if (existLanguage == null)
            {
                throw new CustomException(400, "Name", "Language with this name already exists");
            }
            LanguageDeleteDto languageDeleteDto = new LanguageDeleteDto();
            _mapper.Map(languageDeleteDto, existLanguage);
            _context.Languages.Remove(existLanguage);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<LanguageReturnDto>> GetAllAsync()
        {
            var languages = await _context.Languages.ToListAsync();
            return _mapper.Map<List<LanguageReturnDto>>(languages);
        }

        public async Task<int> UpdateAsync(int id, LanguageUpdateDto languageUpdateDto)
        {
            if (_context.Languages.Any(g => g.Name == languageUpdateDto.Name && g.Id != id))
            {
                throw new CustomException(400, "Name", "Language with this name already exists");
            }
            var existLanguage = await _context.Languages.FindAsync(id);
            if (existLanguage == null)
            {
                throw new CustomException(404, "Language", "Language not found");
            }
            _mapper.Map(languageUpdateDto, existLanguage);
            return await _context.SaveChangesAsync();
        }
    }
}
