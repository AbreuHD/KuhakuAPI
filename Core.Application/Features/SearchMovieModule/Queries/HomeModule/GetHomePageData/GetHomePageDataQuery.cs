using Core.Application.DTOs.General;
using Core.Application.DTOs.Home;
using MediatR;

namespace Core.Application.Features.SearchMovieModule.Queries.HomeModule.GetHomePageData
{
    public class GetHomePageDataQuery : IRequest<GenericApiResponse<HomeDto>>
    {

    }
    public class GetHomePageDataQueryHandler : IRequestHandler<GetHomePageDataQuery, GenericApiResponse<HomeDto>>
    {
        public async Task<GenericApiResponse<HomeDto>> Handle(GetHomePageDataQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
