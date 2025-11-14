using Template.Application.Abstraction.Queries;
using Template.Application.Complaints.Dtos;

namespace Template.Application.Complaints.Queries.GetById;

public class GetComplaintByIdQuery(int complaintId) : IQuery<ComplaintDto>
{
    public int ComplaintId { get; } = complaintId;
}
