using Microsoft.AspNetCore.Mvc;

namespace K_haku_Backend.Controllers
{
    public class ScrapersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
