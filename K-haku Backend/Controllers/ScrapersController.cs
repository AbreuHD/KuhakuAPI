using K_haku.Core.Application.Interface.Services;
using K_haku.Core.Application.Interface.Services.Cuevana;
using K_haku.Core.Application.ViewModels.ScrapPages;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace K_haku_Backend.Controllers
{
    public class ScrapersController : Controller
    {
        private readonly IScrapPagesService _scrapPagesService;
        public ScrapersController(IScrapPagesService scrapPagesService)
        {
            _scrapPagesService = scrapPagesService;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await _scrapPagesService.GetAllAsync());
        }

        public async Task<IActionResult> Add(ScrapPagesViewModel vm)
        {
            await _scrapPagesService.Add(vm);
            return RedirectToRoute(new { controller = "Scrapers", action = "Index" });
        }
    }
}
