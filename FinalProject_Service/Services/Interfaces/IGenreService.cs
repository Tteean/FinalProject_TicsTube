using FinalProject_Service.Dto.ActorDtos;
using FinalProject_Service.Dto.GenreDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Interfaces
{
    public interface IGenreService
    {
        Task<int> CreateAsync(CreateGenreDto createGenreDto);
        Task<List<GenreReturnDto>> GetAllAsync();
        Task<int> UpdateAsync(int id, UpdateGenreDto updateGenreDto);
        Task<int> DeleteAsync(int id);
    }
}
