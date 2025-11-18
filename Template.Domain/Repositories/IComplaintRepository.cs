using Template.Domain.Entities;

namespace Template.Domain.Repositories;

public interface IComplaintRepository : IGenericRepository<Complaint>
{
    Task<List<(Complaint complaint, string userName)>> GetAllComplaintsWithUserName();
    public Task<Complaint?> GetComplaintByIdWithFilesAsync(int complaintId);
}
