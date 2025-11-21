using Template.Domain.Entities;

namespace Template.Domain.Repositories
{
    public interface INotesRepository : IGenericRepository<Note>
    {
        Task<List<(Note note, string userName)>> GetNotesFromComplaintId(int complaintId);
    }
}
