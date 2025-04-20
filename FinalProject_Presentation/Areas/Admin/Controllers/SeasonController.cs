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

        public SeasonController(TicsTubeDbContext context, ISeasonService seasonService)
        {
            _context = context;
            _seasonService = seasonService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SeasonCreateDto seasonCreateDto)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            await _seasonService.CreateAsync(seasonCreateDto);
            return RedirectToAction("Index");
        }
    }
}
