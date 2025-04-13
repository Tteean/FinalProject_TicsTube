using FinalProject_DataAccess.Data;
using FinalProject_Service.Services.Implementations;
using FinalProject_Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_Presentation.Controllers
{
    public class MovieController : Controller
    {
        private readonly TicsTubeDbContext _context;
        private readonly IIMDBService _iMDBService;

        public MovieController(TicsTubeDbContext context, IIMDBService iMDBService)
        {
            _context = context;
            _iMDBService = iMDBService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();
            var existMovie = _context.Movies
                .Include(m => m.Directors)
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieLanguages)
                .ThenInclude(ml => ml.Language)
                .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
                .FirstOrDefault(x => x.Id == id);
            if (existMovie == null)
                return NotFound();
            return View(existMovie);
        }
        public async Task<IActionResult> GetRating(string movieTitle)
        {
            string rating = await _iMDBService.GetIMDbRatingAsync(movieTitle);
            return View("MovieRatingView", rating);
        }
    }
}
