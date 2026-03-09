using HoroscopeChallenge.Application.Commands.Base;
using HoroscopeChallenge.Application.DTOs;

namespace HoroscopeChallenge.Application.Commands.Auth
{
    public record LoginResponse : CommandResponse<LoginResponseDto>;
}
