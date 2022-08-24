using K_haku.Core.Application.Features.ScrapPages.Commands.CreateScrapPages;
using K_haku.Core.Application.Features.ScrapPages.Queries.GetAll;
using K_haku.Core.Application.Interface.Services;
using K_haku.Core.Application.ViewModels.ScrapPages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace K_haku_Backend.Controllers
{
    public class ScrapersController : Controller
    {
        //private readonly IScrapPagesService _scrapPagesService;
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        /*public ScrapersController(IScrapPagesService scrapPagesService)
        {
            _scrapPagesService = scrapPagesService;
        }*/
        
        public async Task<IActionResult> Index()
        {
            return View(await Mediator.Send(new GetAllScrapPagesQuery()));
        }

        public async Task<IActionResult> Add(CreateScrapPagesCommand command)
        {
            await Mediator.Send(command);
            return RedirectToRoute(new { controller = "Scrapers", action = "Index" });
        }
    }
}
