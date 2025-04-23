using AutoMapper;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.EpisodeDtos;
using FinalProject_Service.Dto.GenreDtos;
using FinalProject_Service.Dto.SeasonDtos;
using FinalProject_Service.Services.Implementations;
using FinalProject_Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin,moderator")]
    public class EpisodeController : Controller
    {
        private readonly TicsTubeDbContext _context;
        private readonly IEpisodeService _episodeService;
        private readonly IMapper _mapper;

        public EpisodeController(TicsTubeDbContext context, IEpisodeService episodeService, IMapper mapper)
        {
            _context = context;
            _episodeService = episodeService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View(_context.Episodes.ToList());
        }
        public IActionResult Create()
        {
            ViewBag.Season = _context.Seasons.ToList();
            ViewBag.TVShow = _context.TVShows.ToList();
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EpisodeCreateDto episodeCreateDto)
        {
            ViewBag.Season = _context.Seasons.ToList();
            ViewBag.TVShow = _context.TVShows.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _episodeService.CreateAsync(episodeCreateDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var existEpisode = await _context.Episodes.FindAsync(id);
            if (existEpisode == null)
                return NotFound();
            var episodeUpdateDto = _mapper.Map<EpisodeUpdateDto>(existEpisode);
            ViewBag.Season = _context.Seasons.ToList();
            ViewBag.TVShow = _context.TVShows.ToList();
            return View(episodeUpdateDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] EpisodeUpdateDto episodeUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(episodeUpdateDto);
            }

            await _episodeService.UpdateAsync(id, episodeUpdateDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _episodeService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
