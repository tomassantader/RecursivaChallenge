using HoroscopeChallenge.Application.Commands.User;
using HoroscopeChallenge.Application.Services.UserService;
using HoroscopeChallenge.Domain.Repositories;
using MediatR;
using System.Net;

public class UpdateProfileCommandHandler
    : IRequestHandler<UpdateProfileCommand, UpdateProfileResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _currentUser;

    public UpdateProfileCommandHandler(
        IUserRepository userRepository,
        IUserService currentUser)
    {
        _userRepository = userRepository;
        _currentUser = currentUser;
    }

    public async Task<UpdateProfileResponse> Handle(UpdateProfileCommand request,CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(_currentUser.UserId!.Value, cancellationToken);

        if (user == null)
        {
            return new UpdateProfileResponse
            {
                StatusCode = HttpStatusCode.NotFound
            };
        }

        user.Email = request.Email;
        user.BirthDate = request.BirthDate;

        await _userRepository.Update(user);

        return new UpdateProfileResponse
        {
            Result = true
        };
    }
}