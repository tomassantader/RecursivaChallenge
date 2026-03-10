using HoroscopeChallenge.Domain.Repositories.Base;
using HoroscopeChallenge.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoroscopeChallenge.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public Task Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }
    }
}
