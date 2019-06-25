using Sammo.Sso.Domain.Core.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sammo.Sso.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task ExecuteSqlAsync(string sql, params object[] parameters);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> filter);

        Task<TEntity> FirstOrDefaultAsync();

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter);

        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);

        Task InsertAsync(TEntity entity);

        Task BatcInsertAsync(TEntity[] entities);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
