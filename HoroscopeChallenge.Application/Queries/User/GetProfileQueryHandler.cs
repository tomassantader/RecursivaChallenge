using HoroscopeChallenge.Application.DTOs;
using HoroscopeChallenge.Application.Queries.User;
using HoroscopeChallenge.Application.Services.UserService;
using HoroscopeChallenge.Domain.Repositories;
using MediatR;
using System.Net;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, GetProfileResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _currentUser;

    public GetProfileQueryHandler(
        IUserRepository userRepository,
        IUserService currentUser)
    {
        _userRepository = userRepository;
        _currentUser = currentUser;
    }

    public async Task<GetProfileResponse> Handle(
        GetProfileQuery request,
        CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;

        if (userId == null)
        {
            return new GetProfileResponse
            {
                StatusCode = HttpStatusCode.Unauthorized
            };
        }

        var user = await _userRepository.GetByIdAsync(userId.Value, cancellationToken);

        if (user == null)
        {
            return new GetProfileResponse
            {
                StatusCode = HttpStatusCode.NotFound
            };
        }

        return new GetProfileResponse
        {
            Result = new GetProfileDto
            {
                id = user.Id,
                username = user.Username,
                email = user.Email,
                birthDate = user.BirthDate
            }
        };
    }
}