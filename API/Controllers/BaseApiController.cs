using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // placeholder for the api controller
    public class BaseApiController : ControllerBase
    {
        private IMediator? m_ediator;

        protected IMediator Mediator => m_ediator ??= HttpContext.RequestServices.GetService<IMediator>() ?? throw new InvalidOperationException("IMediator is unavailible");    
    }
}