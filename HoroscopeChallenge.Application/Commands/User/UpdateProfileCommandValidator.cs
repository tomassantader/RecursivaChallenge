using FluentValidation;
using HoroscopeChallenge.Application.Commands.User;
using HoroscopeChallenge.Application.Services.UserService;
using System.Globalization;

namespace HoroscopeChallenge.API.Validators;

public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileCommandValidator(IUserService currentUser)
    {
        RuleFor(_ => currentUser.UserId)
        .NotNull()
        .WithMessage("User must be authenticated.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email inválido");

        RuleFor(x => x.BirthDate)
            .LessThan(DateTime.UtcNow)
            .WithMessage("La fecha de nacimiento no puede ser futura");
    }
}