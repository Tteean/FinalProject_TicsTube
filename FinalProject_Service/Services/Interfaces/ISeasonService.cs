using FinalProject_Service.Dto.SeasonDtos;
using FinalProject_Service.Dto.TVShowDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Interfaces
{
    public interface ISeasonService
    {
        Task<int> CreateAsync(SeasonCreateDto seasonCreateDto);
        Task<List<SeasonReturnDto>> GetAllAsync();
        Task<int> UpdateAsync(int id, SeasonUpdateDto seasonUpdateDto);
        Task<int> DeleteAsync(int id);
    }
}
