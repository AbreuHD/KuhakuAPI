using Auth.Core.Application.DTOs.Generic;
using AutoMapper;
using Core.Application.DTOs.ShareList;
using Core.Application.Interface.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.ShareListModule.Queries
{
    public class UserShareListQuery : IRequest<GenericApiResponse<List<PreviewShareListDto>>>
    {
        public required string User { get; set; }
    }
    public class UserShareListQueryHandler : IRequestHandler<UserShareListQuery, GenericApiResponse<List<PreviewShareListDto>>>
    {
        private readonly IShareListRepository _shareListRepository;
        private readonly IMapper _mapper;
        public UserShareListQueryHandler(IShareListRepository shareListRepository, IMapper mapper)
        {
            _shareListRepository = shareListRepository;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<List<PreviewShareListDto>>> Handle(UserShareListQuery request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<List<PreviewShareListDto>>
            {
                Payload = []
            };

            try
            {
                var lists = await _shareListRepository.GetAllByUserId(request.User, false);
                response.Payload = _mapper.Map<List<PreviewShareListDto>>(lists);
                response.Statuscode = 200;
                response.Message = "Lists found successfully";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                response.Statuscode = 500;
            }

            return response;
        }
    }
}
