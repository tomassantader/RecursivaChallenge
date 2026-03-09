using HoroscopeChallenge.Application.Commands.Auth;
using HoroscopeChallenge.Domain.Entities;
using HoroscopeChallenge.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using HoroscopeChallenge.Application.DTOs;
using HoroscopeChallenge.Application.Services;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly PasswordHasher<User> _passwordHasher = new();

    public LoginCommandHandler(
        IUserRepository userRepository,
        ITokenService jwtService)
    {
        _userRepository = userRepository;
        _tokenService = jwtService;
    }

    public async Task<LoginResponse> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(
            request.Username,
            cancellationToken);

        if (user == null || !PasswordIsValid(user, request.Password))
        {
            return Unauthorized();
        }

        return new LoginResponse
        {
            StatusCode = HttpStatusCode.OK,
            Result = new LoginResponseDto
            {
                id = user.Id,
                username = user.Username,
                email = user.Email,
                birthDate = user.BirthDate,
                token = _tokenService.GenerateToken(user)
            }
        };
    }

    private bool PasswordIsValid(User user, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            password);

        return result == PasswordVerificationResult.Success;
    }

    private static LoginResponse Unauthorized() => new()
    {
        StatusCode = HttpStatusCode.Unauthorized,
        Errors = new Dictionary<string, string[]>
        {
            { "Authentication", new[] { "Usuario y/o contraseña incorrecto" } }
        }
    };
}