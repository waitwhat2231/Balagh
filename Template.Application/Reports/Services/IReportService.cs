using Template.Application.Reports.Dtos;

namespace Template.Application.Reports.Services
{
    public interface IReportService
    {
        public Task<List<ComplaintsStatusReportCountDto>> GetComplaintReportForStatuses(DateTime from, DateTime to, int? govermentalEntityId, string? location);
    }
}
