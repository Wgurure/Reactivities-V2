using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
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

        protected ActionResult HandleResult<T>(Result<T> result){

            if (!result.IsSuccess && result.Code == 404) return NotFound();

            if(result.IsSuccess && result.Value != null ) return Ok(result.Value);

            return BadRequest(result.Error); 
            
        }  
    }
}