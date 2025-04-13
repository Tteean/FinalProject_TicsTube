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
        public IActionResult Search(string search)
        {
            var datas = _context.Movies
                .Include(m => m.Directors)
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieLanguages)
                .ThenInclude(ml => ml.Language)
                .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
                .Where(m => m.Title.Contains(search)
                || m.Directors.FullName.Contains(search)
                || m.MovieGenres.Any(pt => pt.Genre.Name.Contains(search))
                || m.MovieActors.Any(pt => pt.Actor.Fullname.Contains(search))).ToList();
            return PartialView("_SearchPartial", datas);
        }
    }
}
