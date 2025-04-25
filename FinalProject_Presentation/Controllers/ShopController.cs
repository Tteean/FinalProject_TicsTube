using Microsoft.AspNetCore.Mvc;

namespace FinalProject_Presentation.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
