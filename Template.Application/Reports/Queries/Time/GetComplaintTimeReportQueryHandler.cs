using Template.Application.Abstraction.Queries;
using Template.Application.Reports.Dtos;
using Template.Application.Reports.Services;
using Template.Domain.Entities.ResponseEntity;

namespace Template.Application.Reports.Queries.Time
{
    public class GetComplaintTimeReportQueryHandler(IReportService reportService) : IQueryHandler<GetComplaintTimeReportQuery, List<ComplaintTimeBasedReportDto>>
    {
        public async Task<Result<List<ComplaintTimeBasedReportDto>>> Handle(GetComplaintTimeReportQuery request, CancellationToken cancellationToken)
        {
            return Result.Success(await reportService.GetComplaintReportBasedOnTime(request.GovermentalEntityId, request.Status, request.Location));
        }
    }
}
