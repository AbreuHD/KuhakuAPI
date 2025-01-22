using Auth.Core.Application.DTOs.Generic;
using Core.Application.Interface.Repositories;
using MediatR;


namespace Core.Application.Features.ShareListModule.Commands
{
    public class CreateNewListCommand : IRequest<GenericApiResponse<bool>>
    {
        public required string UserId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Img { get; set; }
        public required bool IsPublic { get; set; }
    }
    public class CreateNewListCommandHandler(IShareListRepository shareListRepository) : IRequestHandler<CreateNewListCommand, GenericApiResponse<bool>>
    {
        private readonly IShareListRepository _shareListRepository = shareListRepository;

        public async Task<GenericApiResponse<bool>> Handle(CreateNewListCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<bool>();
            try
            {
                var newList = new Domain.Entities.UserThings.ShareList
                {
                    UserID = request.UserId,
                    Name = request.Name,
                    Description = request.Description ?? string.Empty,
                    Img = request.Img,
                    IsPublic = request.IsPublic
                };

                await _shareListRepository.AddAsync(newList);
                response.Payload = true;
                response.Statuscode = 200;
                response.Message = "List created successfully";
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
