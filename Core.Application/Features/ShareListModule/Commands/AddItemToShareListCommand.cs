using Auth.Core.Application.DTOs.Generic;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Relations;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Application.Features.ShareListModule.Commands
{
    public class AddItemToShareListCommand : IRequest<GenericApiResponse<bool>>
    {
        [JsonIgnore]
        public string? UserId { get; set; }
        public required int ShareListId { get; set; }
        public required int ItemId { get; set; }
        public required bool IsMovie { get; set; }
    }
    public class AddItemToShareListCommandHandler(IMovieList_MovieRepository movieList_MovieRepository, IShareListRepository shareListRepository) : IRequestHandler<AddItemToShareListCommand, GenericApiResponse<bool>>
    {
        private readonly IMovieList_MovieRepository _movieList_MovieRepository = movieList_MovieRepository;
        private readonly IShareListRepository _shareListRepository = shareListRepository;

        public async Task<GenericApiResponse<bool>> Handle(AddItemToShareListCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<bool>();

            try
            {
                var shareRequest = await _shareListRepository.GetByIdAsync(request.ShareListId);
                if (shareRequest.UserID != request.UserId)
                {
                    response.Success = false;
                    response.Statuscode = StatusCodes.Status401Unauthorized;
                    response.Payload = false;
                    response.Message = "You are not authorized to add item to this list";
                    return response;
                }

                if (request.IsMovie) 
                {
                    var movieExist = await _movieList_MovieRepository.ItemExist(request.ItemId, request.ShareListId);
                    if (movieExist)
                    {
                        response.Success = false;
                        response.Statuscode = StatusCodes.Status400BadRequest;
                        response.Payload = false;
                        response.Message = "Item already exist in share list";
                        return response;
                    }
                    else
                    {
                        await _movieList_MovieRepository.AddAsync(new MovieList_Movie
                        {
                            MovieID = request.ItemId,
                            ShareListID = request.ShareListId
                        });
                    }
                }

                response.Success = true;
                response.Statuscode = StatusCodes.Status200OK;
                response.Payload = true;
                response.Message = "Item added to share list successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Statuscode = StatusCodes.Status500InternalServerError;
                response.Payload = true;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
