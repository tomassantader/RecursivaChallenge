using System.Net.Http.Json;
using HoroscopeChallenge.Domain.Entities;
using HoroscopeChallenge.Domain.Interfaces;

public class HoroscopeService : IHoroscopeService
{
    private readonly HttpClient _httpClient;

    public HoroscopeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HoroscopeResponse> GetHoroscopeAsync(string sign, DateTime date)
    {
        var formattedDate = date.ToString("yyyy-MM-dd");

        var url = $"/{sign.ToLower()}/?date={formattedDate}&lang=es";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<HoroscopeResponse>();

        return result;
    }
}