using FinalProject_DataAccess.Data;
using FinalProject_ViewModel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly TicsTubeDbContext _context;

        public HomeController(TicsTubeDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVm vm = new HomeVm();
            vm.Movies = _context.Movies
                .Include(m=>m.MovieGenres).ThenInclude(mg=>mg.Genre)
                .Include(m=>m.MovieActors).ThenInclude(mg=>mg.Actor)
                .Include(m=>m.MovieLanguages).ThenInclude(mg=>mg.Language)
                .ToList();
            vm.Genres = _context.Genres.ToList();

            return View(vm);
        }
    }
}
