using AutoMapper;
using Azure;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.ActorDtos;
using FinalProject_Service.Exceptions;
using FinalProject_Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Implementations
{
    public class ActorService:IActorService
    {
        private readonly TicsTubeDbContext _context;
        private readonly IMapper mapper;

        public ActorService(TicsTubeDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateActorAsync(ActorCreateDto actorCreateDto)
        {
            if (_context.Actors.Any(t => t.Fullname == actorCreateDto.Fullname))
            {
                throw new CustomException(400, "Name", "Actor with this name already exists");
            }
            await _context.Actors.AddAsync(mapper.Map<Actor>(actorCreateDto));
            return await _context.SaveChangesAsync();
        }
        public async Task<List<ActorReturnDto>> GetActorAsync()
        {
            var groups = await _context.Actors.ToListAsync();
            return mapper.Map<List<ActorReturnDto>>(groups);
        }
    }
}
