using HoroscopeChallenge.Application.DTOs;
using HoroscopeChallenge.Application.Queries.Horoscope;
using HoroscopeChallenge.Application.Queries.User;
using HoroscopeChallenge.Application.Services.UserService;
using HoroscopeChallenge.Domain.Repositories;
using MediatR;
using System.Net;

public class GetHoroscopeStatsQueryHandler : IRequestHandler<GetHoroscopeStatsQuery, GetHoroscopeStatsResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _currentUser;
    private readonly IHoroscopeQueryRepository _horoscopeQueryRepository; 

    public GetHoroscopeStatsQueryHandler(
        IUserRepository userRepository,
        IUserService currentUser,
        IHoroscopeQueryRepository horoscopeQueryRepository)
    {
        _userRepository = userRepository;
        _currentUser = currentUser;
        _horoscopeQueryRepository = horoscopeQueryRepository;
    }

    public async Task<GetHoroscopeStatsResponse> Handle(GetHoroscopeStatsQuery request,CancellationToken cancellationToken)
    {
        var history = await _horoscopeQueryRepository.GetHistoryAsync(cancellationToken);

        var mostSearchedSign = history
            .GroupBy(q => q.Sign)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefault();

        var historyResponse = history
            .Select(query => new GetHoroscopeHistoryDto
            {
                UserId = query.UserId,
                Horoscope = query.Horoscope,
                ZodiacSign = query.Sign,
                QueryDate = query.QueryDate
            })
            .ToList();

        return new GetHoroscopeStatsResponse
        {
            StatusCode = HttpStatusCode.OK,
            Result = new GetHoroscopeStatsDto
            {
                MostSearchedZodiacSign = mostSearchedSign,
                SearchHistory = historyResponse
            }
        };
    }
}