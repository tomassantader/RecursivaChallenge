using HoroscopeChallenge.Domain.Entities;

namespace HoroscopeChallenge.Application.Services
{
    public interface ITokenService 
    {
        string GenerateToken(User user);
    }
}
