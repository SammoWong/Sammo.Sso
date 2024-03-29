﻿using Microsoft.EntityFrameworkCore;
using Sammo.Sso.Domain.Core.Models;
using Sammo.Sso.Domain.Interfaces;
using Sammo.Sso.Infrastructure.Data.Context;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sammo.Sso.Infrastructure.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly SsoDbContext _dbContext;

        public Repository(SsoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BatcInsertAsync(TEntity[] entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await Filter(predicate).CountAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ExecuteSqlAsync(string sql, params object[] parameters)
        {
            await _dbContext.Database.ExecuteSqlCommandAsync(sql, parameters);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate = null)
        {
            return Filter(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Filter(predicate).AnyAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        private IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking().AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);

            return query;
        }
    }
}
