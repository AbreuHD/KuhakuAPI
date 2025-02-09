using Auth.Core.Application.DTOs.Generic;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.UserThings;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Core.Application.Features.ShareListModule.Commands
{
    public class EditUserShareListCommand : IRequest<GenericApiResponse<bool>>
    {
        public required int Id { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Img { get; set; }
        public required bool IsPublic { get; set; }
    }
    public class EditUserShareListCommandHandler : IRequestHandler<EditUserShareListCommand, GenericApiResponse<bool>>
    {
        private readonly IShareListRepository _shareListRepository;
        public EditUserShareListCommandHandler(IShareListRepository shareListRepository)
        {
            _shareListRepository = shareListRepository;
        }

        public async Task<GenericApiResponse<bool>> Handle(EditUserShareListCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<bool>();
            try
            {
                var shareListRequest = new ShareList
                {
                    ID = request.Id,
                    UserID = request.UserId!,
                    Name = request.Name,
                    Description = request.Description ?? string.Empty,
                    Img = request.Img,
                    IsPublic = request.IsPublic
                };
                await _shareListRepository.UpdateAsync(shareListRequest, request.Id);
                response.Success = true;
                response.Payload = true;
                response.Message = "ShareList updated successfully";
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
