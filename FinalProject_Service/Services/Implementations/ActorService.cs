using AutoMapper;
using Azure;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.ActorDtos;
using FinalProject_Service.Dto.MovieDtos;
using FinalProject_Service.Exceptions;
using FinalProject_Service.Extentions;
using FinalProject_Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Implementations
{
    public class ActorService:IActorService
    {
        private readonly TicsTubeDbContext _context;
        private readonly IMapper _mapper;
        private readonly PhotoService _photoService;

        public ActorService(TicsTubeDbContext context, IMapper mapper, PhotoService photoService)
        {
            _context = context;
            _mapper = mapper;
            _photoService = photoService;
        }

        public async Task<int> CreateActorAsync(ActorCreateDto actorCreateDto)
        {
            if (_context.Actors.Any(t => t.Fullname == actorCreateDto.Fullname))
            {
                throw new CustomException(400, "Name", "Actor with this name already exists");
            }
            var actor = _mapper.Map<Actor>(actorCreateDto);
            if (actorCreateDto.File != null)
            {
                actor.Image = await _photoService.UploadImageAsync(actorCreateDto.File);
            }
            await _context.Actors.AddAsync(actor);
            return await _context.SaveChangesAsync();
        }


        public async Task<List<ActorReturnDto>> GetActorAsync()
        {
            var actors = await _context.Actors
                .Include(a=>a.MovieActors)
                .ThenInclude(ma=>ma.Movie)
                .ToListAsync();
            return _mapper.Map<List<ActorReturnDto>>(actors);
        }
        public async Task<int> UpdateActorAsync(int id, ActorUpdateDto actorUpdateDto)
        {
            
            if (_context.Actors.Any(g => g.Fullname == actorUpdateDto.Fullname && g.Id != id))
            {
                throw new CustomException(400, "Name", "Actor with this name already exists");
            }
            var existActor = await _context.Actors.FindAsync(id);
            if (existActor == null)
            {
                throw new CustomException(404, "Actor", "Actor not found");
            }
            _mapper.Map(actorUpdateDto, existActor);
            if (actorUpdateDto.File != null)
            {
                if (!string.IsNullOrEmpty(existActor.Image))
                {
                    await _photoService.DeleteAsync(existActor.Image);
                }
                existActor.Image = await _photoService.UploadImageAsync(actorUpdateDto.File);
            }

            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteActorAsync(int id)
        {
            var existActor = await _context.Actors
                .FirstOrDefaultAsync(a => a.Id == id);

            if (existActor == null)
            {
                throw new CustomException(400, "Name", "Actor with this name already exists");
            }
            ActorDeleteDto actorDeleteDto = new ActorDeleteDto();
            _mapper.Map(actorDeleteDto, existActor);
            if (!string.IsNullOrEmpty(existActor.Image))
            {
                await _photoService.DeleteAsync(existActor.Image);
            }

            _context.Actors.Remove(existActor);
            return await _context.SaveChangesAsync();
        }
    }
}
