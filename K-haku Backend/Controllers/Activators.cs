using K_haku.Core.Application.Features.Cuevana.Commands.GetCuevanaMovies;
using K_haku.Core.Application.Interface.Services.Cuevana;
using K_haku.Core.Application.WebsScrapers.GetAll.Cuevana;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace K_haku_Backend.Controllers
{
    public class Activators : Controller
    {
        public readonly ICuevanaMoviesService _cuevanaMoviesService;
        public readonly CuevanaGetAllMovies _cuevanaGetAllMovies;

        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
       /* public Activators(ICuevanaMoviesService cuevanaMoviesService, CuevanaGetAllMovies cuevanaGetAllMovies)
        {
            _cuevanaMoviesService = cuevanaMoviesService;
            _cuevanaGetAllMovies = cuevanaGetAllMovies;
        }*/

        public async Task<IActionResult> CuevanaMovie(GetCuevanaMoviesCommand command)
        {
            command.Start =true;
            //await _cuevanaMoviesService.AddAllAsync(await _cuevanaGetAllMovies.MovieList());
            await Mediator.Send(command);
            return RedirectToRoute(new { controller = "Scrapers", action = "Index" });
        }
    }
}
