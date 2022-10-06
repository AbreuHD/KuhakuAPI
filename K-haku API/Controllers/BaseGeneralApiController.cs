using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace K_haku_API.Controllers
{
        [ApiController]
        [Route("v{version:apiVersion}/[controller]")]
        public abstract class BaseGeneralApiController : ControllerBase
        {
            private IMediator _mediator;
            protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        }
    }