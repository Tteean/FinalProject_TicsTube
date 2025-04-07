using FinalProject_Service.Dto.ActorDtos;
using FinalProject_Service.Dto.MovieDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Interfaces
{
    public interface IMovieService
    {
        Task<int> CreateActorAsync(MovieCreateDto movieCreateDto);
        Task<List<MovieReturnDto>> GetActorAsync();
        Task<int> UpdateActorAsync(int id, MovieUpdateDto movieUpdateDto);
        Task<int> DeleteActorAsync(int id);
    }
}
