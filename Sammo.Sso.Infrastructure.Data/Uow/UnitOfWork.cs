using Microsoft.EntityFrameworkCore.Storage;
using Sammo.Sso.Domain.Interfaces;
using Sammo.Sso.Infrastructure.Data.Context;
using System;

namespace Sammo.Sso.Infrastructure.Data.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SsoDbContext _dbContext;
        private readonly IDbContextTransaction _dbContextTransaction;

        public UnitOfWork(SsoDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContextTransaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
            _dbContextTransaction.Commit();
        }

        public void Rollback()
        {
            _dbContextTransaction.Rollback();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            _dbContextTransaction?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
