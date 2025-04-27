using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.ActorDtos;
using FinalProject_Service.Dto.MovieDtos;
using FinalProject_Service.Extentions;
using FinalProject_Service.Services.Implementations;
using FinalProject_Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace FinalProject_Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin,moderator")]
    public class MovieController : Controller
    {
        private readonly TicsTubeDbContext _context;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public MovieController(IMapper mapper, IMovieService movieService, TicsTubeDbContext context)
        {
            _mapper = mapper;
            _movieService = movieService;
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Movies
                .Include(b => b.Directors)
                .Include(b => b.MovieActors).ThenInclude(ps => ps.Actor)
                .Include(b => b.MovieLanguages).ThenInclude(pt => pt.Language)
                .Include(b => b.MovieGenres).ThenInclude(pt => pt.Genre).ToList());
        }
        public IActionResult Create()
        {
            ViewBag.Directors = _context.Directors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Languages = _context.Languages.ToList();
            ViewBag.Actors = _context.Actors.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] MovieCreateDto movieCreateDto)
        {
            ViewBag.Directors = _context.Directors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Languages = _context.Languages.ToList();
            ViewBag.Actors = _context.Actors.ToList();

            if (!ModelState.IsValid)
                return View();
            await _movieService.CreateAsync(movieCreateDto);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var existMovie = _context.Movies
                .Include(p => p.Directors)
                .Include(p => p.MovieGenres)
                .ThenInclude(ps => ps.Genre)
                .Include(p => p.MovieActors)
                .ThenInclude(pt => pt.Actor)
                .Include(p => p.MovieLanguages)
                .ThenInclude(ps => ps.Language)
                .FirstOrDefault(p => p.Id == id);
            if (existMovie is null)
                return NotFound();

            ViewBag.Directors = _context.Directors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Languages = _context.Languages.ToList();
            ViewBag.Actors = _context.Actors.ToList();
            var movieUpdateDto = _mapper.Map<MovieUpdateDto>(existMovie);
            return View(movieUpdateDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] MovieUpdateDto movieUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            await _movieService.UpdateAsync(id, movieUpdateDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _movieService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
