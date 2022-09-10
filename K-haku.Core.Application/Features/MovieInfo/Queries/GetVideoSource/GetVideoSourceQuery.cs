using K_haku.Core.Application.Dtos.Movie;
using K_haku.Core.Application.WebScrapers.Cuevana.GetVideoSource;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Features.MovieInfo.Queries.GetVideoSource
{
    public class GetVideoSourceQuery : IRequest<string>
    {
        public string MovieLink { get; set; }
        public string Type { get; set; }
    }

    public class GetVideoSourceQueryHandler : IRequestHandler<GetVideoSourceQuery, string>
    {
        CuevanaGetVideoSource CuevanaSource = new();
        public async Task<string> Handle(GetVideoSourceQuery request, CancellationToken cancellationToken)
        {
            VideoMovieResponse response = new();
            response.Link = request.MovieLink;
            response.Type = request.Type;
            return await CuevanaSource.GetSource(response);
        }
    }
}
