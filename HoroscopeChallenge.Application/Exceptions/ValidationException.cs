using FluentValidation.Results;

namespace HoroscopeChallenge.Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
        : base("Uno o mas errores de validacion han ocurrido.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}
