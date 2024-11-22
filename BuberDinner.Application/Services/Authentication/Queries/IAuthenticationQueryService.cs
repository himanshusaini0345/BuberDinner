using BuberDinner.Application.Services.Authentication.Common;

namespace BuberDinner.Application.Services.Authentication.Queries;

public interface IAuthenticationQueryService
{
    AuthenticationResult Login(string email, string password);

}
