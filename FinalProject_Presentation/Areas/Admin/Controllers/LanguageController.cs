using AutoMapper;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.GenreDtos;
using FinalProject_Service.Dto.LanguageDtos;
using FinalProject_Service.Services.Implementations;
using FinalProject_Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin,moderator")]
    public class LanguageController : Controller
    {
        private readonly TicsTubeDbContext _context;
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public LanguageController(IMapper mapper, ILanguageService languageService, TicsTubeDbContext context)
        {
            _mapper = mapper;
            _languageService = languageService;
            _context = context;
        }

        public IActionResult Index()
        {

            return View(_context.Languages.Include(g => g.MovieLanguages).ThenInclude(mg => mg.Movie).ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] LanguageCreateDto languageCreateDto)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            await _languageService.CreateAsync(languageCreateDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var existLanguage = await _context.Languages.FindAsync(id);
            if (existLanguage == null)
                return NotFound();
            var languageUpdateDto = _mapper.Map<LanguageUpdateDto>(existLanguage);
            return View(languageUpdateDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] LanguageUpdateDto languageUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(languageUpdateDto);
            }

            await _languageService.UpdateAsync(id, languageUpdateDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _languageService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
