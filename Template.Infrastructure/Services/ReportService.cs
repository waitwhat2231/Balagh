using Microsoft.EntityFrameworkCore;
using Template.Application.Reports.Dtos;
using Template.Application.Reports.Services;
using Template.Domain;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Services
{
    class ReportService(TemplateDbContext dbContext) : IReportService
    {
        public async Task<List<ComplaintsStatusReportCountDto>> GetComplaintReportForStatuses(DateTime? from = null, DateTime? to = null, int? govermentalEntityId = null, string? location = null)
        {
            var count = await dbContext.Complaints.CountAsync();
            var query = dbContext.Complaints.AsQueryable();
            if (from.HasValue)
            {
                query = query.Where(c => c.CreatedAt >= from);
            }
            if (to.HasValue)
            {
                query = query.Where(c => c.CreatedAt <= to);
            }
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
                ComplaintCount = s.Count(),
                PercentageOfTotalComplaints = (decimal)s.Count() / count * 100
            }).ToListAsync();

        }
        public async Task<List<ComplaintGovermentalEntitiesReportDto>> GetComplaintReportForGovermentalEntities(DateTime? from, DateTime? to, ComplaintStatus? status, string? location)
        {
            var count = await dbContext.Complaints.CountAsync();
            var query = dbContext.Complaints.AsQueryable();
            if (from.HasValue)
            {
                query = query.Where(c => c.CreatedAt >= from);
            }
            if (to.HasValue)
            {
                query = query.Where(c => c.CreatedAt <= to);
            }
            if (status.HasValue)
            {
                query = query.Where(c => c.Status == status);
            }
            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(c => c.Location.Contains(location));
            }
            return await query.GroupBy(c => new
            {
                c.GovernmentalEntityId,
                c.GovernmentalEntity.Name
            }).Select(s => new ComplaintGovermentalEntitiesReportDto()
            {
                GovermentalEntityId = s.Key.GovernmentalEntityId,
                GovermentalEntityName = s.Key.Name,
                ComplaintCount = s.Count(),
                PercentageOfTotalComplaints = (decimal)s.Count() / count * 100
            }).ToListAsync();
        }
        public async Task<List<ComplaintTimeBasedReportDto>> GetComplaintReportBasedOnTime(int? govermentalEntityId, ComplaintStatus? status = null, string? location = null)
        {
            var count = await dbContext.Complaints.CountAsync();
            var query = dbContext.Complaints.AsQueryable();
            if (govermentalEntityId.HasValue)
            {
                query = query.Where(c => c.GovernmentalEntityId == govermentalEntityId);
            }
            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(c => c.Location.Contains(location));
            }
            if (status.HasValue)
            {
                query = query.Where(c => c.Status == status);
            }
            return await query.GroupBy(c => new
            {
                c.CreatedAt.Year,
                c.CreatedAt.Month,
            })
                .Select(s => new ComplaintTimeBasedReportDto()
                {
                    Year = s.Key.Year,
                    Month = s.Key.Month,
                    ComplaintCount = s.Count(),
                    PercentageOfTotalComplaints = (decimal)s.Count() / count * 100
                }).ToListAsync();
        }

    }
}
