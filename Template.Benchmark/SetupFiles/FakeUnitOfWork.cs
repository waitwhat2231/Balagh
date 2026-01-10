using Template.Domain.Repositories;

namespace Template.Benchmark.SetupFiles
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        public Task BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task RollbackAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
