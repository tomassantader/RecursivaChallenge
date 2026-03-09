using HoroscopeChallenge.Domain.Entities.Base;

namespace HoroscopeChallenge.Domain.Entities;

public class HoroscopeQuery : Entity
{
    public int UserId { get; set; }

    public string Sign { get; set; } = null!;

    public string Horoscope { get; set; } = null!;

    public DateTime QueryDate { get; set; }

    public User User { get; set; } = null!;
}