using Template.Application.Abstraction.Queries;
using Template.Application.Reports.Dtos;
using Template.Application.Reports.Services;
using Template.Domain.Entities.ResponseEntity;

namespace Template.Application.Reports.Queries.Statuses
{
    class GetComplaintReportForAllStatusesQueryHandler(IReportService reportService) : IQueryHandler<GetComplaintReportForAllStatusesQuery, List<ComplaintsStatusReportCountDto>>
    {
        public async Task<Result<List<ComplaintsStatusReportCountDto>>> Handle(GetComplaintReportForAllStatusesQuery request, CancellationToken cancellationToken)
        {
            return Result.Success(await reportService.GetComplaintReportForStatuses((DateTime)request.From, (DateTime)request.To, request.GovermentalEntityId, request.Location));
        }
    }
}
