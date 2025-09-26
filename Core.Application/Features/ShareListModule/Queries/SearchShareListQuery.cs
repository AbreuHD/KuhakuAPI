using AutoMapper;
using Core.Application.DTOs.General;
using Core.Application.DTOs.ShareList;
using Core.Application.Interface.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shomei.Infraestructure.Identity.Context;
using Shomei.Infraestructure.Identity.Entities;
using System.Linq;
using System.Net;

namespace Core.Application.Features.ShareListModule.Queries
{
    public class SearchShareListQuery : IRequest<GenericApiResponse<List<PreviewShareListDto>>>
    {
        public string Name { get; set; } = "";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 30;
    }

    public class SearchShareListQueryHandler(IShareListRepository shareListRepository, IMapper mapper, IdentityContext identity)
    : IRequestHandler<SearchShareListQuery, GenericApiResponse<List<PreviewShareListDto>>>
    {
        private readonly IShareListRepository _shareListRepository = shareListRepository;
        private readonly IdentityContext _identity = identity;
        private readonly IMapper _mapper = mapper;

        public async Task<GenericApiResponse<List<PreviewShareListDto>>> Handle(SearchShareListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var lists = await _shareListRepository.SearchShareList(
                    request.Name,
                    request.PageNumber,
                    request.PageSize
                );

                var profileIds = lists.Select(l => l.ProfileId).Distinct().ToList();

                var profiles = await _identity.Set<UserProfile>()
                    .Where(p => profileIds.Contains(p.Id))
                    .ToDictionaryAsync(p => p.Id, p => p.Name, cancellationToken);

                var result = lists.Select(l =>
                {
                    var dto = _mapper.Map<PreviewShareListDto>(l);
                    dto.Username = (l.ProfileId.HasValue && profiles.ContainsKey(l.ProfileId.Value)) ? profiles[l.ProfileId.Value] : "Unknown";
                    return dto;
                }).ToList();

                return new GenericApiResponse<List<PreviewShareListDto>>
                {
                    Payload = result,
                    Message = $"{result.Count} Lists found (Page {request.PageNumber})",
                    Success = true,
                    Statuscode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new GenericApiResponse<List<PreviewShareListDto>>
                {
                    Payload = new List<PreviewShareListDto>(),
                    Message = e.Message,
                    Success = false,
                    Statuscode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
