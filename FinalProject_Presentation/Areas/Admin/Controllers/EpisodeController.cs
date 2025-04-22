using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.EpisodeDtos;
using FinalProject_Service.Dto.SeasonDtos;
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

        public EpisodeController(TicsTubeDbContext context, IEpisodeService episodeService)
        {
            _context = context;
            _episodeService = episodeService;
        }

        public IActionResult Index()
        {
            return View(_context.Episodes.ToList());
        }
        public IActionResult Create()
        {
            ViewBag.Season = _context.Seasons.ToList();
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EpisodeCreateDto episodeCreateDto)
        {
            ViewBag.Season = _context.Seasons.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _episodeService.CreateAsync(episodeCreateDto);
            return RedirectToAction("Index");
        }
    }
}
