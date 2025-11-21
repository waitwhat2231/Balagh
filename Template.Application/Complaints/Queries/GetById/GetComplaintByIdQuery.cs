using Template.Application.Abstraction.Queries;
using Template.Application.Complaints.Dtos;

namespace Template.Application.Complaints.Queries.GetById;

public class GetComplaintByIdQuery(int complaintId, bool? includeNotes = false) : IQuery<ComplaintDto>
{
    public int ComplaintId { get; } = complaintId;
    public bool? IncludeNotes { get; } = includeNotes;
}
