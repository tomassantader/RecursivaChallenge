using HoroscopeChallenge.Application.Commands.User;
using HoroscopeChallenge.Application.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene el perfil del usuario autenticado.
    /// </summary>
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var response = await _mediator.Send(new GetProfileQuery());
        return Ok(response);
    }

    /// <summary>
    /// Actuliza email y fecha de nacimiento del usuario autenticado.
    /// </summary>
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile(UpdateProfileCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}