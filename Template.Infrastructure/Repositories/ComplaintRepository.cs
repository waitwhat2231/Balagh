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
            .Include(c => c.GovernmentalEntity)
            .Include(c => c.User)
            .Include(c => c.Histories)
                .ThenInclude(h => h.User)
            .Include(c => c.ComplaintFiles)
            .FirstOrDefaultAsync(c => c.Id == complaintId);
    }
    public async Task<PagedEntity<GetAllComplaintsMappingDto>> GetAllComplaintsWithUserName(int pageNum, int pageSize, EnumRoleNames userRole, string UserId)
    {
        var query = dbContext.Complaints
            .Select(c =>
            new
            {
                Complaint = c,
                Username = c.User.UserName,
                LockedByUserName = c.LockedByUser.UserName,
                GovermentalEntityName = c.GovernmentalEntity.Name,
            }
            );
        switch (userRole)
        {
            case EnumRoleNames.User:
                query = query.Where(x => x.Complaint.UserId == UserId);
                break;
            case EnumRoleNames.Employee:
                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == UserId);
                query = query.Where(x => x.Complaint.GovernmentalEntityId == user.GovernmentalEntityId);
                break;
            case EnumRoleNames.Administrator:
                break;
        }
        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.Complaint.CreatedAt)
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Select(x => new GetAllComplaintsMappingDto
            {
                Id = x.Complaint.Id,
                UserId = x.Complaint.UserId,
                UserName = x.Username!,
                LockedBy = x.Complaint.LockedBy,
                LockedByUserName = x.LockedByUserName,
                Description = x.Complaint.Description,
                Location = x.Complaint.Location,
                Status = x.Complaint.Status,
                GovernmentalEntityId = x.Complaint.GovernmentalEntityId,
                IsLocked = x.Complaint.IsLocked,
                CreatedAt = x.Complaint.CreatedAt,
                GovermentalEntityName = x.GovermentalEntityName
            })
            .ToListAsync();

        return new PagedEntity<GetAllComplaintsMappingDto>()
        {
            Items = items,
            TotalItems = totalCount,
            PageNumber = pageNum,
            PageSize = pageSize,
        };
    }
}
