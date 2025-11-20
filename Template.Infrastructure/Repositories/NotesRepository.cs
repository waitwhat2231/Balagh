using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories
{
    public class NotesRepository(TemplateDbContext dbContext) : GenericRepository<Note>(dbContext), INotesRepository
    {
    }
}
