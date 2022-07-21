using K_haku.Core.Application.Interface.Services.Cuevana;
using K_haku.Core.Movie.GetAll.Cuevana;
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
            var MoviesList = _cuevanaGetAllMovies.MovieList();
            foreach(var movie in MoviesList)
            {
            await _cuevanaMoviesService.Add(movie);

            }
            return RedirectToRoute(new { controller = "Scrapers", action = "Index" });
        }
    }
}
