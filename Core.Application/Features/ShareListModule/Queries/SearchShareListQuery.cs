using AutoMapper;
using Core.Application.DTOs.General;
using Core.Application.DTOs.ShareList;
using Core.Application.Interface.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Features.ShareListModule.Queries
{
    public class SearchShareListQuery : IRequest<GenericApiResponse<List<PreviewShareListDto>>>
    {
        public string? Name { get; set; }
    }
    public class SearchShareListQueryHandler : IRequestHandler<SearchShareListQuery, GenericApiResponse<List<PreviewShareListDto>>>
    {
        private readonly IShareListRepository _shareListRepository;
        private readonly IMapper _mapper;
        public SearchShareListQueryHandler(IShareListRepository shareListRepository, IMapper mapper)
        {
            _shareListRepository = shareListRepository;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<List<PreviewShareListDto>>> Handle(SearchShareListQuery request, CancellationToken cancellationToken)
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
                var lists = await _shareListRepository.SearchShareList(request.Name ?? String.Empty);
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
