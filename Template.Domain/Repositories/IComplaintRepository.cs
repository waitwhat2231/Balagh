using Template.Domain.Entities;
using Template.Domain.Enums;
using Template.Domain.Pagination;

namespace Template.Domain.Repositories;

public interface IComplaintRepository : IGenericRepository<Complaint>
{
    Task<PagedEntity<GetAllComplaintsMappingDto>> GetAllComplaintsWithUserName(int pageNum, int pageSize, EnumRoleNames userRole, string UserId);
    public Task<Complaint?> GetComplaintByIdWithFilesAsync(int complaintId);
}
