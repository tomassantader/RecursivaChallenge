using HoroscopeChallenge.Domain.Entities;

namespace HoroscopeChallenge.Domain.Interfaces
{
    public interface IHoroscopeService
    {
        Task<HoroscopeResponse?> GetHoroscopeAsync(string sign, DateTime date);
    }
}
