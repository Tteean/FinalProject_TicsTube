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

        public ActorController(TicsTubeDbContext context, IActorService actorService)
        {
            _context = context;
            _actorService = actorService;
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
                return View(kdjdjirmm jrfj                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              );
            }
            await _actorService.CreateActorAsync(actorCreateDto);
           return RedirectToAction("Index", "Dashboard");

        }
        
        

    }
}
