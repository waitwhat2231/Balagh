using Microsoft.Extensions.Caching.Memory;
using Template.Domain.Entities;
using Template.Domain.Enums;
using Template.Domain.Pagination;
using Template.Domain.Repositories;

namespace Template.Infrastructure.Repositories;

public class CachedComplaintRepository : IComplaintRepository
{
    private readonly ComplaintRepository _decorated;
    private readonly IMemoryCache _memoryCache;
    public CachedComplaintRepository(ComplaintRepository decorated, IMemoryCache memoryCache)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }

    public async Task<Complaint?> GetComplaintByIdWithFilesAsync(int complaintId)
    {
        string key = $"complaint-{complaintId}";

        return await _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                return _decorated.GetComplaintByIdWithFilesAsync(complaintId);
            });
    }

    public async Task<Complaint> AddAsync(Complaint entity)
    {
        var created = await _decorated.AddAsync(entity);
        return created;
    }

    public async Task<Complaint?> FindByIdAsync(int id)
    {
        string key = $"complaint-{id}";

        return await _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                return _decorated.FindByIdAsync(id);
            });
    }

    public async Task<IEnumerable<Complaint>> GetAllAsync()
    {
        return await _decorated.GetAllAsync();
    }



    public Task<IEnumerable<Complaint>> GetPagedResponseAsync(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task HardDeleteAsync(Complaint entity)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync()
    {
        await _decorated.SaveChangesAsync();
    }

    public Task SoftDeleteAsync(Complaint entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Complaint entity)
    {
        throw new NotImplementedException();
    }

    public Task<PagedEntity<(Complaint complaint, string userName)>> GetAllComplaintsWithUserName(int pageNum, int pageSize, EnumRoleNames userRole, string UserId)
    {
        return _decorated.GetAllComplaintsWithUserName(pageNum, pageSize, userRole, UserId); ;
    }
}
