using FinalProject_Service.Dto.SeasonDtos;
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
    }
}
