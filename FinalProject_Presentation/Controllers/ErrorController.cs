using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject_Presentation.Controllers
{
    public class ErrorController : Controller
    {
    [Route("Error")]
        public IActionResult Error()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (feature != null)
            {
                ViewBag.ErrorMessage = feature.Error.Message;
            }
            return View();
        }
    }
}
