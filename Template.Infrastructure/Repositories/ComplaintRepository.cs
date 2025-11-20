using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Enums;
using Template.Domain.Pagination;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class ComplaintRepository : GenericRepository<Complaint>, IComplaintRepository
{
    public ComplaintRepository(TemplateDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<Complaint?> GetComplaintByIdWithFilesAsync(int complaintId)
    {
        return await dbContext.Complaints
            .Include(c => c.User)
            .Include(c => c.Histories)
                .ThenInclude(h => h.User)
            .Include(c => c.ComplaintFiles)
            .FirstOrDefaultAsync(c => c.Id == complaintId);
    }
    public async Task<PagedEntity<(Complaint complaint, string userName)>> GetAllComplaintsWithUserName(int pageNum, int pageSize, EnumRoleNames userRole, string UserId)
    {
        var query = dbContext.Complaints.AsQueryable();
        switch (userRole)
        {
            case EnumRoleNames.User:
                query = query.Where(c => c.UserId == UserId);
                break;
            case EnumRoleNames.Employee:
                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == UserId);
                query = query.Where(c => c.GovernmentalEntityId == user.GovernmentalEntityId);
                break;
            case EnumRoleNames.Administrator:
                break;

        }
        var complaintList = await query.Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new ValueTuple<Complaint, string>(c, c.User.UserName))
            .ToListAsync();
        var pagedEntityResult = new PagedEntity<(Complaint complaint, string userName)>()
        {
            Items = complaintList,
            PageNumber = pageNum,
            PageSize = pageSize,
            TotalItems = await dbContext.Complaints.CountAsync()
        };
        return pagedEntityResult;
    }
}
