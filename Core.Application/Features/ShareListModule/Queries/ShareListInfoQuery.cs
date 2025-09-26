using AutoMapper;
using Core.Application.DTOs.General;
using Core.Application.DTOs.ShareList;
using Core.Application.Interface.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shomei.Infraestructure.Identity.Context;
using Shomei.Infraestructure.Identity.Entities;
using System.Security.Principal;

namespace Core.Application.Features.ShareListModule.Queries
{
    public class ShareListInfoQuery : IRequest<GenericApiResponse<PreviewShareListDto>>
    {
        public required int Id { get; set; }
    }
    public class ShareListInfoQueryHandler(IShareListRepository shareListRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, 
        IdentityContext identity) : IRequestHandler<ShareListInfoQuery, GenericApiResponse<PreviewShareListDto>>
    {
        private readonly IShareListRepository _shareListRepository = shareListRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IdentityContext _identity = identity;
        public async Task<GenericApiResponse<PreviewShareListDto>> Handle(ShareListInfoQuery request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<PreviewShareListDto>
            {
                Payload = new(),
                Success = true,
                Statuscode = StatusCodes.Status200OK,
                Message = string.Empty
            };

            try
            {
                var lists = await _shareListRepository.GetByIdWithMoviesAsync(request.Id);
                var user = _httpContextAccessor.HttpContext?.User;
                var profileClaim = user?.FindFirst("ProfileId")?.Value;

                int.TryParse(profileClaim, out int profileIdValue);

                if (lists.IsPublic is false && lists.ProfileId != profileIdValue)
                {
                    response.Message = "List not found";
                    response.Success = false;
                    response.Statuscode = StatusCodes.Status404NotFound;
                    return response;
                }
                response.Payload = _mapper.Map<PreviewShareListDto>(lists);
                response.Payload.Username = _identity.Set<UserProfile>()
                    .Where(p => p.Id == lists.ProfileId)
                    .Select(p => p.Name)
                    .FirstOrDefault() ?? "Unknown";
                response.Statuscode = StatusCodes.Status200OK;
                response.Message = "Lists found successfully";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                response.Statuscode = StatusCodes.Status500InternalServerError;
            }

            return response;
        }
    }
}
