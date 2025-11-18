using Template.Application.Abstraction.Queries;
using Template.Application.Complaints.Dtos;
using Template.Domain.Pagination;

namespace Template.Application.Complaints.Queries.GetAll;

public class GetAllComplaintsQuery : IQuery<PagedEntity<ComplaintDto>>
{
    public int PageNum { get; set; } = 1;
    public int PageSize { get; set; }
}
