using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using LoginRequest = BuberDinner.Contracts.Authentication.LoginRequest;
using RegisterRequest = BuberDinner.Contracts.Authentication.RegisterRequest;

namespace BuberDinner.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        public IActionResult Register( [FromBody]RegisterRequest request)
        {
            var authResult = _authenticationService.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);

            var response = new AuthenticationResponse(
                authResult.Id,
                authResult.FirstName,
                authResult.LastName,
                authResult.Email,
                authResult.Token);

            return Ok(response);
        }
        [HttpPost("Login")]
        public IActionResult Login( [FromBody]LoginRequest request)
        {
            var authResult = _authenticationService.Login(
                request.Email,
                request.Password);

            var response = new AuthenticationResponse(
                authResult.Id,
                authResult.FirstName,
                authResult.LastName,
                authResult.Email,
                authResult.Token);

            return Ok(response);
        }
        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok("Test successful");
        }

    }
}
