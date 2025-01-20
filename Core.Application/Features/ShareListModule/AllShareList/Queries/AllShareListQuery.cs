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

namespace Core.Application.Features.ShareListModule.AllShareListQuery.Queries
{
    public class AllShareListQuery : IRequest<GenericApiResponse<List<PreviewShareListDto>>>
    {
    }
    public class AllShareListQueryHandler : IRequestHandler<AllShareListQuery, GenericApiResponse<List<PreviewShareListDto>>>
    {
        private readonly IShareListRepository _shareListRepository;
        private readonly IMapper _mapper;
        public AllShareListQueryHandler(IShareListRepository shareListRepository, IMapper mapper)
        {
            _shareListRepository = shareListRepository;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<List<PreviewShareListDto>>> Handle(AllShareListQuery request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<List<PreviewShareListDto>>
            {
                Payload = []
            };

            try
            {
                var lists = await _shareListRepository.GetAllAsync();
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
