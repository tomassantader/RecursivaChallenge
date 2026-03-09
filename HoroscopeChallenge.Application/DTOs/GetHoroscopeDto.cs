namespace HoroscopeChallenge.Application.DTOs;
public record GetHoroscopeDto
{
    public string? ZodiacSign { get; set; }
    public string? Horoscope { get; set; }
    public DateTime HoroscopeDate { get; set; }
    public int DaysUntilNextBirthday { get; set; }
}

