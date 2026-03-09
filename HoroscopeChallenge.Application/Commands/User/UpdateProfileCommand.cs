using MediatR;

namespace HoroscopeChallenge.Application.Commands.User;
public record UpdateProfileCommand : IRequest<UpdateProfileResponse>
{
    public string Email { get; init; } = string.Empty;
    public DateTime BirthDate { get; init; }
}
