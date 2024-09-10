using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KuhakuCentral.Controllers.General
{
    [ApiController]
    [Route("v{version:apiVersion}/es/[controller]")]
    public abstract class BaseAPI : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
