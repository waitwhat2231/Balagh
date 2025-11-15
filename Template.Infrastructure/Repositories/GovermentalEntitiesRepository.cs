using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories
{
    public class GovermentalEntitiesRepository(TemplateDbContext dbContext) : GenericRepository<GovernmentalEntity>(dbContext), IGovermentalEntitiesRepository
    {
    }
}
