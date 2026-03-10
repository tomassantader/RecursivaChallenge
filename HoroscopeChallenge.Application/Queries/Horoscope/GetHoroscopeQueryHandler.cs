using HoroscopeChallenge.Application.DTOs;
using HoroscopeChallenge.Application.Queries.Horoscope;
using HoroscopeChallenge.Application.Queries.User;
using HoroscopeChallenge.Application.Services.UserService;
using HoroscopeChallenge.Application.Utils;
using HoroscopeChallenge.Domain.Entities;
using HoroscopeChallenge.Domain.Interfaces;
using HoroscopeChallenge.Domain.Repositories;
using HoroscopeChallenge.Infrastructure.Repositories;
using MediatR;
using System.Net;

public class GetHoroscopeQueryHandler : IRequestHandler<GetHoroscopeQuery, GetHoroscopeResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _currentUser;
    private readonly IHoroscopeService _horoscopeService;
    private readonly IHoroscopeCacheRepository _horoscopeCacheRepository; 
    private readonly IHoroscopeQueryRepository _horoscopeQueryRepository; 

    public GetHoroscopeQueryHandler(
        IUserRepository userRepository,
        IUserService currentUser,
        IHoroscopeService horoscopeService,
        IHoroscopeCacheRepository horoscopeCacheRepository,
        IHoroscopeQueryRepository horoscopeQueryRepository)
    {
        _userRepository = userRepository;
        _currentUser = currentUser;
        _horoscopeService = horoscopeService;
        _horoscopeCacheRepository = horoscopeCacheRepository;
        _horoscopeQueryRepository = horoscopeQueryRepository;
    }

    public async Task<GetHoroscopeResponse> Handle(GetHoroscopeQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(_currentUser.UserId!.Value, cancellationToken);
        
        if (user == null) return new GetHoroscopeResponse { StatusCode = HttpStatusCode.NotFound };

        var sign = Helpers.GetSign(user.BirthDate);
        var today = DateTime.UtcNow.Date;

        var cached = await _horoscopeCacheRepository.GetBySignAndDate(sign, today, cancellationToken);

        var horoscope = cached?.Horoscope
                        ?? (await _horoscopeService.GetHoroscopeAsync(sign, DateTime.UtcNow))?.Horoscope;

        if (horoscope == null) return new GetHoroscopeResponse { StatusCode = HttpStatusCode.NotFound };

        if (cached == null)
        {
            await _horoscopeCacheRepository.AddAsync(new HoroscopeCache { Sign = sign, Date = today, Horoscope = horoscope });
        }

        await _horoscopeQueryRepository.AddAsync(new HoroscopeQuery
        {
            UserId = user.Id,
            Sign = sign,
            Horoscope = horoscope,
            QueryDate = DateTime.UtcNow
        });

        return new GetHoroscopeResponse
        {
            Result = new GetHoroscopeDto
            {
                Horoscope = horoscope,
                ZodiacSign = sign,
                DaysUntilNextBirthday = Helpers.GetDaysUntilNextBirthday(user.BirthDate),
                HoroscopeDate = DateTime.UtcNow
            }
        };
    }
}