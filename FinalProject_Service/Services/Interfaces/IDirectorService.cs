using FinalProject_Service.Dto.DirectorDtos;
using FinalProject_Service.Dto.GenreDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Interfaces
{
    public interface IDirectorService
    {
        Task<int> CreateAsync(DirectorCreateDto directorCreateDto);
        Task<List<DirectorReturnDto>> GetAllAsync();
        Task<int> UpdateAsync(int id, DirectorUpdateDto directorUpdateDto);
        Task<int> DeleteAsync(int id);
    }
}
