using HoroscopeChallenge.Domain.Entities.Base;

namespace HoroscopeChallenge.Domain.Entities;
public class User : Entity
{
    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public ICollection<HoroscopeQuery> Queries { get; set; } = new List<HoroscopeQuery>();
}
