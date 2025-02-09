using Auth.Core.Application.DTOs.Generic;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.UserThings;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Features.ShareListModule.Commands
{
    public class DelShareListCommand : IRequest<GenericApiResponse<bool>>
    {
        public required int Id { get; set; }
        public required string UserId { get; set; }
    }
    public class DelShareListCommandHandler : IRequestHandler<DelShareListCommand, GenericApiResponse<bool>>
    {
        private readonly IShareListRepository _shareListRepository;
        public DelShareListCommandHandler(IShareListRepository shareListRepository)
        {
            _shareListRepository = shareListRepository;
        }
        public async Task<GenericApiResponse<bool>> Handle(DelShareListCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<bool>();
            try
            {
                var shareListRequest = new ShareList
                {
                    Name = "ShareList",
                    Img = "ShareList",
                    IsPublic = true,
                    UserID = request.UserId,
                    ID = request.Id
                };
                await _shareListRepository.DeleteAsync(shareListRequest);
                response.Success = true;
                response.Payload = true;
                response.Message = "ShareList deleted successfully";
                response.Statuscode = StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Payload = false;
                response.Message = ex.Message;
                response.Statuscode = StatusCodes.Status500InternalServerError;
            }
            return response;
        }
    }
}
