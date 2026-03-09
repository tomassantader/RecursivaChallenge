using HoroscopeChallenge.Application.Commands.Base;
using HoroscopeChallenge.Application.DTOs;

namespace HoroscopeChallenge.Application.Queries.User
{
    public record GetHoroscopeResponse : CommandResponse<GetHoroscopeDto>;
}
