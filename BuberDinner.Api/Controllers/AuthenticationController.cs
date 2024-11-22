using Microsoft.AspNetCore.Mvc;
using BuberDinner.Contracts.Authentication;
using LoginRequest = BuberDinner.Contracts.Authentication.LoginRequest;
using RegisterRequest = BuberDinner.Contracts.Authentication.RegisterRequest;
using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Authentication.Queries;
using BuberDinner.Application.Services.Authentication.Common;

namespace BuberDinner.Api.Controllers;

[Route("auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationCommandService _authenticationService;
    private readonly IAuthenticationQueryService _authenticationQueryService;

    public AuthenticationController(IAuthenticationCommandService authenticationService, IAuthenticationQueryService authenticationQueryService)
    {
        _authenticationService = authenticationService;
        _authenticationQueryService = authenticationQueryService;
    }

    [HttpPost("register")]
    public IActionResult Register( [FromBody]RegisterRequest request)
    {
        var authResult = _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        var response = ToAuthenticationResult(authResult);

        return Ok(response);
    }

    private static AuthenticationResponse ToAuthenticationResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
                    authResult.User.Id,
                    authResult.User.FirstName,
                    authResult.User.LastName,
                    authResult.User.Email,
                    authResult.Token);
    }

    [HttpPost("login")]
    public IActionResult Login( [FromBody]LoginRequest request)
    {
        var authResult = _authenticationQueryService.Login(
            request.Email,
            request.Password);

        var response = ToAuthenticationResponse(authResult);

        return Ok(response);
    }

    private static AuthenticationResponse ToAuthenticationResponse(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
                    authResult.User.Id,
                    authResult.User.FirstName,
                    authResult.User.LastName,
                    authResult.User.Email,
                    authResult.Token);
    }
}
