using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Notifications;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class DeviceRepository(TemplateDbContext dbContext) : GenericRepository<Device>(dbContext), IDeviceRepository
{
    public async Task<List<Device>?> SearchAsync(string? deviceToken, string? userId, bool? optIn, DateTime? loggedInInAfter, DateTime? loggedInBefore)
    {
        var query = dbContext.Devices.AsQueryable();

        if (deviceToken != null) query = query.Where(d => d.DeviceToken == deviceToken);

        if (userId != null) query = query.Where(d => d.UserId == userId);

        if (optIn != null) query = query.Where(d => d.OptIn == optIn);

        if (loggedInInAfter != null) query = query.Where(d => d.LastLoggedInAt <= loggedInInAfter);

        if (loggedInBefore != null) query = query.Where(d => d.LastLoggedInAt >= loggedInBefore);

        var device = await query.ToListAsync();
        return device;
    }
    public async Task<Device?> GetDeviceByToken(string DeviceToken, string? userId)
    {
        var query = await dbContext.Devices
            .Include(d => d.User)
            .FirstOrDefaultAsync(d => d.DeviceToken == DeviceToken);

        return query;
    }
}
