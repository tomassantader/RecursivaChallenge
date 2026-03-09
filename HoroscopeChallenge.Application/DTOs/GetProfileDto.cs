namespace HoroscopeChallenge.Application.DTOs;
public record GetProfileDto
{
    public int id { get; set; }
    public string username { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public DateTime birthDate { get; set; }
}

