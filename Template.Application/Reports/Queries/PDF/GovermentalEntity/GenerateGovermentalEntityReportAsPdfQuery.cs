using MediatR;
using Template.Domain;

namespace Template.Application.Reports.Queries.PDF.GovermentalEntity
{
    public class GenerateGovermentalEntityReportAsPdfQuery(DateTime? from, DateTime? to, int? govermentalEntityId = null, string? location = null, ComplaintStatus? status = null) : GetComplaintReportQueryBase(from, to, govermentalEntityId, location, status), IRequest<byte[]>
    {
    }
}
