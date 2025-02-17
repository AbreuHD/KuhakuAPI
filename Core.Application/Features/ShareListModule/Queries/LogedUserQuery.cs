using AutoMapper;
using Core.Application.DTOs.General;
using Core.Application.DTOs.ShareList;
using Core.Application.Interface.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Features.ShareListModule.Queries
{
    public class LogedUserQuery : IRequest<GenericApiResponse<List<PreviewShareListDto>>>
    {
        public required string UserId { get; set; }
    }
    public class LogedUserQueryHandler(IShareListRepository shareListRepository, IMapper mapper) : IRequestHandler<LogedUserQuery, GenericApiResponse<List<PreviewShareListDto>>>
    {
        private readonly IShareListRepository _shareListRepository = shareListRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<GenericApiResponse<List<PreviewShareListDto>>> Handle(LogedUserQuery request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<List<PreviewShareListDto>>
            {
                Payload = [],
                Success = true,
                Statuscode = StatusCodes.Status200OK,
                Message = string.Empty
            };

            try
            {
                var lists = await _shareListRepository.GetAllByUserId(request.UserId, true);
                response.Payload = _mapper.Map<List<PreviewShareListDto>>(lists);
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
