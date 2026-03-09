using HoroscopeChallenge.Application.Commands.Auth;
using MediatR;

public class LoginCommand : IRequest<LoginResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
}