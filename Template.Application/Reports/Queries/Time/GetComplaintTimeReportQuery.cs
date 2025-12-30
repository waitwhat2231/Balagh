using Template.Application.Abstraction.Queries;
using Template.Application.Reports.Dtos;
using Template.Domain;

namespace Template.Application.Reports.Queries.Time
{
    public class GetComplaintTimeReportQuery(int? govermentalEntityId = null, string? location = null, ComplaintStatus? status = null) : GetComplaintReportQueryBase(null, null, govermentalEntityId, location, status), IQuery<List<ComplaintTimeBasedReportDto>>
    {
    }
}
