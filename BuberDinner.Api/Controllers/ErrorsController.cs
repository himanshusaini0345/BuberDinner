﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BuberDinner.Application.Common.Exceptions;

namespace BuberDinner.Api.Controllers
{
    public class ErrorsController : ControllerBase
    {
        [HttpGet]
        [Route("/error")]
        public IActionResult Error()
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            var (statusCode, message) = exception switch
            {
                IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
                Exception ex => (StatusCodes.Status400BadRequest, ex.Message),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error.")
            };

            return Problem(title: message, statusCode: statusCode);
        }
    }   
}
