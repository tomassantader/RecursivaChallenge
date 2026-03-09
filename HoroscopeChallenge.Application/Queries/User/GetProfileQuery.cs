using MediatR;

namespace HoroscopeChallenge.Application.Queries.User
{
    public record GetProfileQuery : IRequest<GetProfileResponse>;
}