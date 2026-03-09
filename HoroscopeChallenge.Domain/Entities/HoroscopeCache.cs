using HoroscopeChallenge.Domain.Entities.Base;

namespace HoroscopeChallenge.Domain.Entities;

public class HoroscopeCache: Entity
{
    public string Sign { get; set; } = null!;

    public DateTime Date { get; set; }

    public string Horoscope { get; set; } = null!;
}