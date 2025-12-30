using Template.Application.Abstraction.Queries;
using Template.Application.Reports.Dtos;
using Template.Application.Reports.Services;
using Template.Domain.Entities.ResponseEntity;

namespace Template.Application.Reports.Queries.GovermentalEntities
{
    public class GetComplaintReportForAllGovermentalEntitiesQueryHandler(IReportService reportService) : IQueryHandler<GetComplaintReportForAllGovermentalEntitiesQuery, List<ComplaintGovermentalEntitiesReportDto>>
    {
        public async Task<Result<List<ComplaintGovermentalEntitiesReportDto>>> Handle(GetComplaintReportForAllGovermentalEntitiesQuery request, CancellationToken cancellationToken)
        {
            return Result.Success(await reportService.GetComplaintReportForGovermentalEntities((DateTime)request.From, (DateTime)request.To, request.Status, request.Location));
        }
    }
}
