using AutoMapper;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.MovieDtos;
using FinalProject_Service.Dto.TVShowDtos;
using FinalProject_Service.Services.Implementations;
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
        private readonly IMapper _mapper;

        public TVShowController(ITVShowService tvShowService, TicsTubeDbContext context, IMapper mapper)
        {
            _tvShowService = tvShowService;
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var tvShow=_context.TVShows
                .Include(t => t.Directors)
                .Include(t => t.TVShowLanguages).ThenInclude(tl => tl.Language)
                .Include(t => t.TVShowActors).ThenInclude(ta => ta.Actor)
                .Include(t => t.TVShowGenres).ThenInclude(tg => tg.Genre)
                .Include(t=>t.Seasons).ThenInclude(t=>t.Episodes).ToList();
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
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var existShow = _context.TVShows
                .Include(p => p.Directors)
                .Include(p => p.TVShowGenres)
                .ThenInclude(ps => ps.Genre)
                .Include(p => p.TVShowActors)
                .ThenInclude(pt => pt.Actor)
                .Include(p => p.TVShowLanguages)
                .ThenInclude(ps => ps.Language)
                .FirstOrDefault(p => p.Id == id);
            if (existShow is null)
                return NotFound();

            ViewBag.Directors = _context.Directors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Languages = _context.Languages.ToList();
            ViewBag.Actors = _context.Actors.ToList();
            var tVShowUpdateDto = _mapper.Map<TVShowUpdateDto>(existShow);
            return View(tVShowUpdateDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] TVShowUpdateDto tVShowUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            await _tvShowService.UpdateAsync(id, tVShowUpdateDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _tvShowService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
