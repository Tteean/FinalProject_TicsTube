using FinalProject_DataAccess.Data;
using FinalProject_ViewModel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_Presentation.Controllers
{
    public class ShopController : Controller
    {
        private readonly TicsTubeDbContext _context;

        public ShopController(TicsTubeDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ShopVm vm = new ShopVm();
            vm.Products = _context.Products.ToList();
            ViewBag.Movies = _context.Movies.ToList();
            ViewBag.TVShow = _context.TVShows.ToList();
            return View(vm);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();
            var existProduct = _context.Products
                .FirstOrDefault(x => x.Id == id);
            if (existProduct == null)
                return NotFound();
            return View(existProduct);
        }

    }
}
