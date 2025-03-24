using FinalProject_Core.ViewModels;
using FinalProject_DataAccess.Data;
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
            vm.Movies = _context.Movies.Include(m=>m.MovieImages).ToList();
            return View();
        }
    }
}
