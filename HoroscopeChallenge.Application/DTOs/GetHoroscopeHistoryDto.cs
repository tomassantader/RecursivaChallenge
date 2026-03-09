namespace HoroscopeChallenge.Application.DTOs;
public record GetHoroscopeHistoryDto
{
    public int UserId { get; set; }
    public string? ZodiacSign { get; set; }
    public string? Horoscope { get; set; }
    public DateTime QueryDate { get; set; }
}
