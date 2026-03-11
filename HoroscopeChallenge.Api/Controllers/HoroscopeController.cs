using HoroscopeChallenge.Application.Queries.Horoscope;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/horoscope")]
[Authorize]
public class HoroscopeController : ControllerBase
{
    private readonly IMediator _mediator;

    public HoroscopeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene el horóscopo del día del usuario autenticado según su signo zodiacal.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetHoroscope()
    {
        var result = await _mediator.Send(new GetHoroscopeQuery());

        return Ok(result);
    }

    /// <summary>
    /// Obtiene historial de consultas y signo mas buscado.
    /// </summary>
    [HttpGet("stats")]
    public async Task<IActionResult> GetHoroscopeStats()
    {
        var result = await _mediator.Send(new GetHoroscopeStatsQuery());

        return Ok(result);
    }
}