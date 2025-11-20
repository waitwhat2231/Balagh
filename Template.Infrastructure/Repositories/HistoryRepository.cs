using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories
{
    public class HistoryRepository(TemplateDbContext dbContext) : GenericRepository<History>(dbContext), IHistoryRepository
    {
    }
}
