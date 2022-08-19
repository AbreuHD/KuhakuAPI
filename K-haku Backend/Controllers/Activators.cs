using K_haku.Core.Application.Interface.Services.Cuevana;
using K_haku.Core.Application.WebsScrapers.GetAll.Cuevana;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace K_haku_Backend.Controllers
{
    public class Activators : Controller
    {
        public readonly ICuevanaMoviesService _cuevanaMoviesService;
        public readonly CuevanaGetAllMovies _cuevanaGetAllMovies;
        public Activators(ICuevanaMoviesService cuevanaMoviesService, CuevanaGetAllMovies cuevanaGetAllMovies)
        {
            _cuevanaMoviesService = cuevanaMoviesService;
            _cuevanaGetAllMovies = cuevanaGetAllMovies;
        }

        public async Task<IActionResult> CuevanaMovie()
        {
            await _cuevanaMoviesService.AddAllAsync(await _cuevanaGetAllMovies.MovieList());
            return RedirectToRoute(new { controller = "Scrapers", action = "Index" });
        }
    }
}
