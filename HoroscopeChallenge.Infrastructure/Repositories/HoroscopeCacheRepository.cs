using HoroscopeChallenge.Domain.Entities;
using HoroscopeChallenge.Domain.Repositories;
using HoroscopeChallenge.Infrastructure.Persistence;
using HoroscopeChallenge.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoroscopeChallenge.Infrastructure.Repositories
{
    public class HoroscopeCacheRepository : Repository<HoroscopeCache>, IHoroscopeCacheRepository
    {

        public HoroscopeCacheRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<HoroscopeCache?> GetBySignAndDate(string sign, DateTime date, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(
                    x => x.Sign == sign && x.Date == date,
                    cancellationToken);
        }
    }
}

