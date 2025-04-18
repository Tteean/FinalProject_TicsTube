using FinalProject_Service.Dto.MovieDtos;
using FinalProject_Service.Dto.TVShowDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Interfaces
{
    public interface ITVShowService
    {
        Task<int> CreateAsync(TVShowCreateDto tVShowCreateDto);
        Task<List<TVShowReturnDto>> GetAllAsync();
        Task<int> UpdateAsync(int id, TVShowUpdateDto tVShowUpdateDto);
        Task<int> DeleteAsync(int id);
    }
}
