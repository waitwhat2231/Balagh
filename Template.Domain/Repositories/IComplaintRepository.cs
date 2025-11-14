using Template.Domain.Entities;

namespace Template.Domain.Repositories;

public interface IComplaintRepository : IGenericRepository<Complaint>
{
    public Task<Complaint?> GetComplaintByIdWithFilesAsync(int complaintId);
}
