using MediatR;
using Template.Domain;

namespace Template.Application.Reports.Queries.PDF.TimeBased
{
    public class GenerateTimeBasedReportPdfQuery(int? govermentalEntityId = null, string? location = null, ComplaintStatus? status = null) : GetComplaintReportQueryBase(null, null, govermentalEntityId, location, status), IRequest<byte[]>
    {
    }
}
