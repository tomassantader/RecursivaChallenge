using HoroscopeChallenge.Application.Commands.Auth;
using MediatR;

public class LoginCommand : IRequest<LoginResponse>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}