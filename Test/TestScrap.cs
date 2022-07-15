using K_haku.Core.Movie.GetAll.Cuevana;
using K_haku.Core.Movie.GetVideos.Cuevana;
using System;

namespace Test
{
    internal class TestScrap
    {
        static void Main(string[] args)
        {
            CuevanaGetAllMovies cuevana = new();
            //cuevana.MovieList();
            CuevanaGetAllVideos cuevanaVid = new();
            cuevanaVid.MovieVideos("https://ww1.cuevana3.me/75/whiplash");
        }
    }
}
