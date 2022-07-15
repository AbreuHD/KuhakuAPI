using K_haku.Core.Movie.GetAll.Cuevana;
using System;

namespace Test
{
    internal class TestScrap
    {
        static void Main(string[] args)
        {
            CuevanaGetAllMovies cuevana = new();
            cuevana.MovieList();
        }
    }
}
