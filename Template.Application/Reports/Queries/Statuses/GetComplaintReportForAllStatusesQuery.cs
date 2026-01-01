using Template.Application.Abstraction.Queries;
using Template.Application.Reports.Dtos;
using Template.Domain;

namespace Template.Application.Reports.Queries.Statuses
{
    public class GetComplaintReportForAllStatusesQuery(DateTime? from, DateTime? to, int? govermentalEntityId = null, string? location = null, ComplaintStatus? status = null) : GetComplaintReportQueryBase(from, to, govermentalEntityId, location, status), IQuery<List<ComplaintsStatusReportCountDto>>
    {

    }
}
