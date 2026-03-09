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

    [HttpGet("horoscope")]
    public async Task<IActionResult> GetHoroscope()
    {
        var result = await _mediator.Send(new GetHoroscopeQuery());

        return Ok(result);
    }

    [HttpGet("horoscopeStats")]
    public async Task<IActionResult> GetHoroscopeStats()
    {
        var result = await _mediator.Send(new GetHoroscopeStatsQuery());

        return Ok(result);
    }
}