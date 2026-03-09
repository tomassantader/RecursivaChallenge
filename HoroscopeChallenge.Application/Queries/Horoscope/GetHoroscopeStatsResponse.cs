using HoroscopeChallenge.Application.Commands.Base;
using HoroscopeChallenge.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoroscopeChallenge.Application.Queries.Horoscope
{
    public record GetHoroscopeStatsResponse : CommandResponse<GetHoroscopeStatsDto>;
}
