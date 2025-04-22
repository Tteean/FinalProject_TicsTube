using FinalProject_Service.Dto.EpisodeDtos;
using FinalProject_Service.Dto.SeasonDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Interfaces
{
    public interface IEpisodeService
    {
        Task<int> CreateAsync(EpisodeCreateDto episodeCreateDto);
        Task<List<EpisodeReturnDto>> GetAllAsync();
        Task<int> UpdateAsync(int id, EpisodeUpdateDto episodeUpdateDto);
        Task<int> DeleteAsync(int id);
    }
}
