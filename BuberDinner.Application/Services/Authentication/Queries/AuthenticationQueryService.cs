using BuberDinner.Domain.Entities;
using BuberDinner.Application.Common.Exceptions;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Services.Authentication.Common;

namespace BuberDinner.Application.Services.Authentication.Queries;

public class AuthenticationQueryService : IAuthenticationQueryService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Login(string email, string password)
    {
        // 1. Validate the user does exist
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User with this email doesn't exist.");
        }

        // 2. Validate the password is correct
        if (user.Password != password)
        {
            throw new Exception("Invalid passsword.");
        }

        // 3. Create Jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}
