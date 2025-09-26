using KuhakuCentral.Controllers.General;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shomei.Infraestructure.Identity.DTOs.Generic;
using Shomei.Infraestructure.Identity.Features.UserProfile.Commands;
using Shomei.Infraestructure.Identity.Features.UserProfile.Queries;
using Shomei.Infraestructure.Identity.Middleware;

namespace KuhakuCentral.Controllers.V1.Account
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProfileController(IMediator mediator) : BaseApi
    {
        public new IMediator Mediator { get; } = mediator;

        [HttpPost("select")]
        [Authorize]
        public async Task<IActionResult> SelectProfile([FromBody] SelectProfileQuery request)
        {
            var response = await Mediator.Send(request);
            return StatusCode(response.Statuscode, response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProfiles()
        {
            var response = await Mediator.Send(new GetProfilesQuery());
            return StatusCode(response.Statuscode, response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUserProfile([FromBody] CreateUserProfileCommand request)
        {
            var response = await Mediator.Send(request);
            return StatusCode(response.Statuscode, response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserProfile([FromRoute] int id, [FromBody] string password)
        {
            var response = await Mediator.Send(new DeleteUserProfileCommand { Id = id, Password = password });
            return StatusCode(response.Statuscode, response);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> EditUserProfile([FromRoute] int id, [FromBody] EditUserProfileCommand request)
        {
            request.Id = id;
            var response = await Mediator.Send(request);
            return StatusCode(response.Statuscode, response);
        }
    }
}
