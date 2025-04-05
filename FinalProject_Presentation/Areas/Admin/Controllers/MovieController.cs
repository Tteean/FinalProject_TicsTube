using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject_Presentation.Areas.Admin.Controllers
{
    public class MovieController : Controller
    {
        [Area("Admin")]
        [Authorize(Roles = "admin,moderator")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
