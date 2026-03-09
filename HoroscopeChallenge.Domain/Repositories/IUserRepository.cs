using HoroscopeChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoroscopeChallenge.Domain.Repositories.Base;

namespace HoroscopeChallenge.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);

    //Task<User?> GetByEmailAsync(string email);
}
