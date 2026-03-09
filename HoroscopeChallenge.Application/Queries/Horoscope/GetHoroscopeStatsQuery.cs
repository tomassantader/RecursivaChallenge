using HoroscopeChallenge.Application.Queries.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoroscopeChallenge.Application.Queries.Horoscope
{
    public class GetHoroscopeStatsQuery : IRequest<GetHoroscopeStatsResponse>
    {
    }
}
