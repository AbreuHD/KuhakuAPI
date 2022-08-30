using Microsoft.AspNetCore.Mvc;

namespace K_haku_API.Controllers.V1
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
