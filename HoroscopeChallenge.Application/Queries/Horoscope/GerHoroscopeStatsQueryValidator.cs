using FluentValidation;
using HoroscopeChallenge.Application.Services.UserService;

namespace HoroscopeChallenge.Application.Queries.Horoscope
{
    public class GetHoroscopeStatsQueryValidator : AbstractValidator<GetHoroscopeStatsQuery>
    {
        public GetHoroscopeStatsQueryValidator(IUserService currentUser)
        {
            RuleFor(_ => currentUser.UserId)
                .NotNull()
                .WithMessage("User must be authenticated.");
        }
    }
}
