using Microsoft.EntityFrameworkCore.Storage;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories
{
    class UnitOfWork(TemplateDbContext dbContext) : IUnitOfWork
    {
        private IDbContextTransaction? dbContextTransaction { get; set; }
        public async Task BeginTransactionAsync()
        {
            dbContextTransaction = await dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await dbContext.SaveChangesAsync();
                await dbContextTransaction?.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            dbContextTransaction?.Dispose();
            dbContextTransaction = null;
        }

        public async Task RollbackAsync()
        {
            if (dbContextTransaction != null)
                await dbContextTransaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
