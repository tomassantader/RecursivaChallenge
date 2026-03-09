using HoroscopeChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoroscopeChallenge.Domain.Repositories.Base;

namespace HoroscopeChallenge.Domain.Repositories;

public interface IHoroscopeCacheRepository : IRepository<HoroscopeCache>
{
        Task<HoroscopeCache?> GetBySignAndDate(string sign,DateTime date, CancellationToken cancellationToken = default);
}