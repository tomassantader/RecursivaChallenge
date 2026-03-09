using System.Net;

namespace HoroscopeChallenge.Application.Commands.Base;

public record CommandResponse<T>
{
    public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.OK;
    public IDictionary<string, string[]>? Errors { get; init; }
    public T? Result { get; init; }
}