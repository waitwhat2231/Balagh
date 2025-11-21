using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories
{
    public class NotesRepository(TemplateDbContext dbContext) : GenericRepository<Note>(dbContext), INotesRepository
    {
        public async Task<List<(Note note, string userName)>> GetNotesFromComplaintId(int complaintId)
        {
            return await dbContext.Notes.Where(n => n.ComplaintId == complaintId)
                .Select(n => new ValueTuple<Note, string>(n, n.User.UserName)).ToListAsync();
        }
    }
}
