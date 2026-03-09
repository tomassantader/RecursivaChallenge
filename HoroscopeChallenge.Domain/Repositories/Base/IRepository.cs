using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HoroscopeChallenge.Domain.Repositories.Base
{
    public interface IRepository<T>
    {
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        Task Update(T entity);

        Task DeleteAsync(T entity);
    }
}
