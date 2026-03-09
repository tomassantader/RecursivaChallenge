using HoroscopeChallenge.Application.Commands.Base;
using HoroscopeChallenge.Application.Exceptions;
using System.Net;

namespace HoroscopeChallenge.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var response = new CommandResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = ex.Errors,
                    Result = null
                };

                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new CommandResponse<object>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new Dictionary<string, string[]>
                {
                    { "Server", new[] { "Ha ocurrido un error inesperado." } }
                }
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
