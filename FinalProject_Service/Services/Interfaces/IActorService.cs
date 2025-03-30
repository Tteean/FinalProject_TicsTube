using FinalProject_Service.Dto.ActorDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Interfaces
{
    public interface IActorService
    {
        Task<int> CreateActorAsync(ActorCreateDto actorCreateDto);
        Task<List<ActorReturnDto>> GetActorAsync();

    }
}
