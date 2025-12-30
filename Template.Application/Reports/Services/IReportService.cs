using Template.Application.Reports.Dtos;
using Template.Domain;

namespace Template.Application.Reports.Services
{
    public interface IReportService
    {
        Task<List<ComplaintGovermentalEntitiesReportDto>> GetComplaintReportForGovermentalEntities(DateTime from, DateTime to, ComplaintStatus? status, string? location);
        public Task<List<ComplaintsStatusReportCountDto>> GetComplaintReportForStatuses(DateTime from, DateTime to, int? govermentalEntityId, string? location);
        public Task<List<ComplaintTimeBasedReportDto>> GetComplaintReportBasedOnTime(int? govermentalEntityId = null, ComplaintStatus? status = null, string? location = null);
    }
}
