using Microsoft.AspNetCore.Mvc;

namespace FinalProject_Presentation.Areas.Admin.Controllers
{
    public class SeasonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
