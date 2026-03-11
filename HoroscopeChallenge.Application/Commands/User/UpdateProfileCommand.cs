using MediatR;

namespace HoroscopeChallenge.Application.Commands.User;
public record UpdateProfileCommand : IRequest<UpdateProfileResponse>
{
    public string? Email { get; init; }
    public DateTime? BirthDate { get; init; }
}
