using Microsoft.EntityFrameworkCore;
using Template.Application.Reports.Dtos;
using Template.Application.Reports.Services;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Services
{
    class ReportService(TemplateDbContext dbContext) : IReportService
    {
        public async Task<List<ComplaintsStatusReportCountDto>> GetComplaintReportForStatuses(DateTime from, DateTime to, int? govermentalEntityId, string? location)
        {
            var query = dbContext.Complaints.Where(c => c.CreatedAt > from && c.CreatedAt <= to).AsQueryable();
            if (govermentalEntityId.HasValue)
            {
                query = query.Where(c => c.GovernmentalEntityId == govermentalEntityId);
            }
            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(c => c.Location.Contains(location));
            }
            return await query.GroupBy(c => c.Status).Select(s => new ComplaintsStatusReportCountDto()
            {
                Status = s.Key,
                Count = s.Count()
            }).ToListAsync();

        }
    }
}
