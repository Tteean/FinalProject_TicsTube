using AutoMapper;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.GenreDtos;
using FinalProject_Service.Dto.SeasonDtos;
using FinalProject_Service.Services.Implementations;
using FinalProject_Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject_Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin,moderator")]
    public class SeasonController : Controller
    {
        private readonly TicsTubeDbContext _context;
        private readonly ISeasonService _seasonService;
        private readonly IMapper _mapper;

        public SeasonController(TicsTubeDbContext context, ISeasonService seasonService, IMapper mapper)
        {
            _context = context;
            _seasonService = seasonService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View(_context.Seasons.ToList());
        }
        public IActionResult Create()
        {
            ViewBag.TVShow = _context.TVShows.ToList();
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SeasonCreateDto seasonCreateDto)
        {
            ViewBag.TVShow = _context.TVShows.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _seasonService.CreateAsync(seasonCreateDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var existSeason = await _context.Seasons.FindAsync(id);
            if (existSeason == null)
                return NotFound();
            var seasonUpdateDto = _mapper.Map<SeasonUpdateDto>(existSeason);
            ViewBag.TVShow = _context.TVShows.ToList();
            return View(seasonUpdateDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] SeasonUpdateDto seasonUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(seasonUpdateDto);
            }

            await _seasonService.UpdateAsync(id, seasonUpdateDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _seasonService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
