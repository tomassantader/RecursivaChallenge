using HoroscopeChallenge.Domain.Entities;
using HoroscopeChallenge.Domain.Repositories;
using HoroscopeChallenge.Infrastructure.Repositories.Base;
using HoroscopeChallenge.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HoroscopeChallenge.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}
