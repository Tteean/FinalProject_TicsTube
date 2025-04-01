using AutoMapper;
using Azure;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.ActorDtos;
using FinalProject_Service.Exceptions;
using FinalProject_Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin,moderator")]
    public class ActorController : Controller
    {
        private readonly TicsTubeDbContext _context;
        private readonly IActorService _actorService;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public ActorController(TicsTubeDbContext context, IActorService actorService, IWebHostEnvironment env, IMapper mapper)
        {
            _context = context;
            _actorService = actorService;
            _env = env;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View(_context.Actors.Include(a => a.MovieActors).ThenInclude(ma => ma.Movie).ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ActorCreateDto actorCreateDto)
        {
            
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _actorService.CreateActorAsync(actorCreateDto);
           return RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var existActor = await _context.Actors.FindAsync(id);
            if (existActor == null) 
                return NotFound();
            var actorUpdateDto = _mapper.Map<ActorUpdateDto>(existActor);
            return View(actorUpdateDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody]int id,[FromForm] ActorUpdateDto actorUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(actorUpdateDto);
            }

            await _actorService.UpdateActorAsync(id, actorUpdateDto);
            return RedirectToAction("Index", "Dashboard");
        }



    }
}
