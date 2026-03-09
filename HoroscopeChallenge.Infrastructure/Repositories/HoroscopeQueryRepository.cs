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
    public class HoroscopeQueryRepository : Repository<HoroscopeQuery>, IHoroscopeQueryRepository
    {

        public HoroscopeQueryRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<List<HoroscopeQuery>> GetHistoryAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.HoroscopeQueries
                .AsNoTracking()
                .OrderByDescending(q => q.QueryDate)
                .ToListAsync(cancellationToken);
        }
    }
}
