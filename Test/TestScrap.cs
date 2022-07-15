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
            List<CuevanaVideoViewModel> videolink = cuevanaVid.MovieVideos("https://ww1.cuevana3.me/58842/lightyear");

            foreach (CuevanaVideoViewModel video in videolink)
            {
                if(video.Type.Length != 0 || video.Language != "Unknow")
                {
                    string vidFinal = await cuevanaVid.GetSource(video);
                    Console.WriteLine(vidFinal);
                    Console.WriteLine("-----------------------------------------------------");
                    Console.WriteLine("");
                }
            }
        }
    }
}
