using Template.Domain.Entities;
using Template.Domain.Enums;
using Template.Domain.Pagination;

namespace Template.Domain.Repositories;

public interface IComplaintRepository : IGenericRepository<Complaint>
{
    Task<PagedEntity<GetAllComplaintsMappingDto>> GetAllComplaintsWithUserName(int pageNum, int pageSize, EnumRoleNames userRole, string UserId);
    public Task<Complaint?> GetComplaintByIdWithFilesAsync(int complaintId);
    public void ApplyConcurrencyCheck(Complaint complaint, byte[] rowVersion);
    public Task<Complaint?> GetComplaintByIdWithDetailsAsync(int complaintId);
    Task RemoveFileAsync(int fileId);
    /*    public Task AddFileAsync(ComplaintFile complaintFile);
public Task DeleteFileAsync(int complaintFileId);*/
}
