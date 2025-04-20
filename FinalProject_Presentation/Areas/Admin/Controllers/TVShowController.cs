using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.MovieDtos;
using FinalProject_Service.Dto.TVShowDtos;
using FinalProject_Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin,moderator")]
    public class TVShowController : Controller
    {
        private readonly TicsTubeDbContext _context;
        private readonly ITVShowService _tvShowService;
        public IActionResult Index()
        {
            var tvShow=_context.TVShows
                .Include(b => b.Directors)
                .Include(b => b.TVShowLanguages).ThenInclude(ps => ps.Language)
                .Include(b => b.TVShowActors).ThenInclude(pt => pt.Actor)
                .Include(b => b.TVShowGenres).ThenInclude(pt => pt.Genre);
            return View(tvShow);
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
        public async Task<IActionResult> Create([FromForm] TVShowCreateDto tVShowCreateDto)
        {
            ViewBag.Directors = _context.Directors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Languages = _context.Languages.ToList();
            ViewBag.Actors = _context.Actors.ToList();

            if (!ModelState.IsValid)
                return View();
            await _tvShowService.CreateAsync(tVShowCreateDto);
            return RedirectToAction("Index");
        }
    }
}
