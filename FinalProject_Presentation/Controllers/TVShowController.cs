using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Services.Interfaces;
using FinalProject_ViewModel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_Presentation.Controllers
{
    public class TVShowController : Controller
    {
        private readonly TicsTubeDbContext _context;
        private readonly IIMDBService _iMDBService;
        private readonly UserManager<AppUser> _userManager;

        public TVShowController(UserManager<AppUser> userManager, IIMDBService iMDBService, TicsTubeDbContext context)
        {
            _userManager = userManager;
            _iMDBService = iMDBService;
            _context = context;
        }

        public IActionResult Index()
        {
            TVShowVm vm = new TVShowVm();
            vm.TVShows = _context.TVShows
                .Include(m => m.TVShowGenres).ThenInclude(mg => mg.Genre)
                .Include(m => m.TVShowActors).ThenInclude(mg => mg.Actor)
                .Include(m => m.TVShowLanguages).ThenInclude(mg => mg.Language)
                .ToList();
            vm.Genres = _context.Genres.ToList();

            return View(vm);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();
            var existShow = _context.TVShows
                .Include(m => m.Directors)
                .Include(m => m.TVShowGenres)
                .ThenInclude(mg => mg.Genre)
                .Include(m => m.TVShowLanguages)
                .ThenInclude(ml => ml.Language)
                .Include(m => m.TVShowActors)
                .ThenInclude(ma => ma.Actor)
                .FirstOrDefault(x => x.Id == id);
            if (existShow == null)
                return NotFound();
            string imdbRating = await _iMDBService.GetIMDbRatingAsync(existShow.Title);
            ViewBag.Rating = imdbRating;
            return View(existShow);
        }
    }
}
