using K_haku.Core.Application.ViewModels.Cuevana;
using K_haku.Core.Movie.GetAll.Cuevana;
using K_haku.Core.Movie.GetVideos.Cuevana;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test
{
    internal class TestScrap
    {
        static async Task Main(string[] args)
        {
            CuevanaGetAllMovies cuevana = new();
            //cuevana.MovieList();
            CuevanaGetAllVideos cuevanaVid = new();
            Console.WriteLine("Ingrese la url de la pelicula");
            var url = Console.ReadLine();
            List <CuevanaVideoViewModel> videolink = cuevanaVid.MovieVideos(url);

            foreach (CuevanaVideoViewModel video in videolink)
            {
                try
                {
                    string vidFinal = await cuevanaVid.GetSource(video);
                    Console.WriteLine(vidFinal);
                    Console.WriteLine("-----------------------------------------------------");
                    Console.WriteLine("");
                }
                catch (Exception ex) { }
            }
        }
    }
}
