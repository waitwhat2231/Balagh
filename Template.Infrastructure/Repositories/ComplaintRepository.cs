using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
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
            .Include(c => c.ComplaintFiles)
            .FirstOrDefaultAsync(c => c.Id == complaintId);
    }
    public async Task<List<(Complaint complaint, string userName)>> GetAllComplaintsWithUserName()
    {
        return await dbContext.Complaints.Select(c => new ValueTuple<Complaint, string>(c, c.User.UserName)).ToListAsync();
    }
}
