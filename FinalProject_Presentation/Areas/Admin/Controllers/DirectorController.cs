using AutoMapper;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.DirectorDtos;
using FinalProject_Service.Dto.LanguageDtos;
using FinalProject_Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin,moderator")]
    public class DirectorController : Controller
    {
        private readonly TicsTubeDbContext _context;
        private readonly IDirectorService _directorService;
        private readonly IMapper _mapper;

        public DirectorController(IMapper mapper, TicsTubeDbContext context, IDirectorService directorService)
        {
            _mapper = mapper;
            _context = context;
            _directorService = directorService;
        }

        public IActionResult Index()
        {

            return View(_context.Directors.Include(g => g.Movies).ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DirectorCreateDto directorCreateDto)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            await _directorService.CreateAsync(directorCreateDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var existDirector = await _context.Directors.FindAsync(id);
            if (existDirector == null)
                return NotFound();
            var directorUpdateDto = _mapper.Map<DirectorUpdateDto>(existDirector);
            return View(directorUpdateDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] DirectorUpdateDto directorUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(directorUpdateDto);
            }

            await _directorService.UpdateAsync(id, directorUpdateDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _directorService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
