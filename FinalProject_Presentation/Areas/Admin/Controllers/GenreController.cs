using AutoMapper;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.ActorDtos;
using FinalProject_Service.Dto.GenreDtos;
using FinalProject_Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin,moderator")]
    public class GenreController : Controller
    {
        private readonly TicsTubeDbContext _context;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(IMapper mapper, TicsTubeDbContext context, IGenreService genreService)
        {
            _mapper = mapper;
            _context = context;
            _genreService = genreService;
        }
        public IActionResult Index()
        {

            return View(_context.Genres.Include(g => g.MovieGenres).ThenInclude(mg => mg.Movie).ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateGenreDto createGenreDto)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            await _genreService.CreateAsync(createGenreDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var existGenre = await _context.Genres.FindAsync(id);
            if (existGenre == null)
                return NotFound();
            var updateGenreDto = _mapper.Map<UpdateGenreDto>(existGenre);
            return View(updateGenreDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] UpdateGenreDto updateGenreDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateGenreDto);
            }

            await _genreService.UpdateAsync(id, updateGenreDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _genreService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
