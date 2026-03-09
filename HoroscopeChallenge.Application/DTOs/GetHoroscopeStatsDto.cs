namespace HoroscopeChallenge.Application.DTOs;
public record GetHoroscopeStatsDto
{
    public string? MostSearchedZodiacSign { get; set; }
    public List<GetHoroscopeHistoryDto>? SearchHistory { get; set; }
}
