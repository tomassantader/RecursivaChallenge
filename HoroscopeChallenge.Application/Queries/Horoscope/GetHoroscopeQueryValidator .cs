using FluentValidation;
using HoroscopeChallenge.Application.Services.UserService;

namespace HoroscopeChallenge.Application.Queries.Horoscope
{
    public class GetHoroscopeQueryValidator : AbstractValidator<GetHoroscopeQuery>
    {
        public GetHoroscopeQueryValidator(IUserService currentUser)
        {
            RuleFor(_ => currentUser.UserId)
                .NotNull()
                .WithMessage("User must be authenticated.");
        }
    }
}
