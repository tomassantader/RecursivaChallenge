using HoroscopeChallenge.Application.Services.UserService;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId
    {
        get
        {
            var claim = _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                return null;

            return int.Parse(claim.Value);
        }
    }
}