using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace K_haku_API.Controllers
{
        [ApiController]
        [Route("v{version:apiVersion}/es/[controller]")]
        public abstract class BaseESApiController : ControllerBase
        {
            private IMediator _mediator;
            protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        }
    }