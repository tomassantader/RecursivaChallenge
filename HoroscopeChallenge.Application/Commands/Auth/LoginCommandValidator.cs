using FluentValidation;
using HoroscopeChallenge.Application.Commands.User;

namespace HoroscopeChallenge.API.Validators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("EL usuario es obligatorio");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria");

    }
}