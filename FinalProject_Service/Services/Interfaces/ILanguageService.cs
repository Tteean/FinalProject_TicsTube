using FinalProject_Service.Dto.GenreDtos;
using FinalProject_Service.Dto.LanguageDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Interfaces
{
    public interface ILanguageService
    {
        Task<int> CreateAsync(LanguageCreateDto languageCreateDto);
        Task<List<LanguageReturnDto>> GetAllAsync();
        Task<int> UpdateAsync(int id, LanguageUpdateDto languageUpdateDto);
        Task<int> DeleteAsync(int id);
    }
}
