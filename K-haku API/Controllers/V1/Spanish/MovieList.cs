using Microsoft.AspNetCore.Mvc;

namespace K_haku_API.Controllers.V1.Spanish
{
    public class MovieList : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
