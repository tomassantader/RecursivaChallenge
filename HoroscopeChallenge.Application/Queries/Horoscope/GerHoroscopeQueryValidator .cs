using FluentValidation;
using HoroscopeChallenge.Application.Services.UserService;

namespace HoroscopeChallenge.Application.Queries.Horoscope
{
    public class GerHoroscopeQueryValidator : AbstractValidator<GetHoroscopeQuery>
    {
        public GerHoroscopeQueryValidator(IUserService currentUser)
        {
            RuleFor(_ => currentUser.UserId)
                .NotNull()
                .WithMessage("User must be authenticated.");
        }
    }
}
