using Core.Application.DTOs.General;
using Core.Application.Interface.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Core.Application.Features.ShareListModule.Commands
{
    public class DelItemFromShareList : IRequest<GenericApiResponse<bool>>
    {
        [JsonIgnore]
        public string? UserId { get; set; }
        public required int ListId { get; set; }
        public required int ItemId { get; set; }
    }
    public class DelItemFromShareListHandler : IRequestHandler<DelItemFromShareList, GenericApiResponse<bool>>
    {
        private readonly IMovieList_MovieRepository _movieList_MovieRepository;
        private readonly IShareListRepository _shareListRepository;

        public DelItemFromShareListHandler(IMovieList_MovieRepository movieList_MovieRepository, IShareListRepository shareListRepository)
        {
            _movieList_MovieRepository = movieList_MovieRepository;
            _shareListRepository = shareListRepository;
        }

        public async Task<GenericApiResponse<bool>> Handle(DelItemFromShareList request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<bool>()
            {
                Payload = true,
                Success = true,
                Statuscode = StatusCodes.Status200OK,
                Message = string.Empty
            };

            try
            {
                if (!await _movieList_MovieRepository.ItemExist(request.ItemId, request.ListId))
                {
                    response.Success = false;
                    response.Statuscode = StatusCodes.Status500InternalServerError;
                    response.Payload = false;
                    response.Message = "Item not found in share list";
                    return response;
                }
                var shareRequest = await _shareListRepository.GetByIdAsync(request.ListId);
                if (shareRequest.UserID != request.UserId)
                {
                    response.Success = false;
                    response.Statuscode = StatusCodes.Status401Unauthorized;
                    response.Payload = false;
                    response.Message = "You are not authorized to delete item from this list";
                    return response;
                }
                await _movieList_MovieRepository.GetByIdAsync(request.ItemId);
                response.Success = true;
                response.Statuscode = StatusCodes.Status200OK;
                response.Payload = true;
                response.Message = "Item deleted from share list";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Statuscode = StatusCodes.Status500InternalServerError;
                response.Payload = false;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }
    }
}
