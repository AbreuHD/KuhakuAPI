using K_haku.Core.Application.Dtos.Movie;
using K_haku.Core.Application.WebsScrapers.GetVideos.Cuevana;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Features.MovieInfo.Queries.GetVideoList
{
    public class GetVideoListQuery : IRequest<List<MovieVideoResponse>>
    {
        public string MovieLink { get; set; }
    }
    public class GetVideoListQueryHandler : IRequestHandler<GetVideoListQuery, List<MovieVideoResponse>>
    {
        CuevanaGetAllVideos CuevanaVideo = new();

        public Task<List<MovieVideoResponse>> Handle(GetVideoListQuery request, CancellationToken cancellationToken)
        {
            return CuevanaVideo.MovieVideos(request.MovieLink);
        }
    }
}
