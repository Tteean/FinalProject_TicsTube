using FinalProject_DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin,moderator")]
    public class SubscribersController : Controller
    {
        private readonly TicsTubeDbContext _context;

        public SubscribersController(TicsTubeDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Subscribers.ToList());
        }
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Subscribers.FirstOrDefaultAsync(s=>s.Id==id);
            _context.Subscribers.Remove(user);
            return RedirectToAction("Index");
        }
    }
}
