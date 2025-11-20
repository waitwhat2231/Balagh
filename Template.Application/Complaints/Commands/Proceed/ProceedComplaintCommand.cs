using Template.Application.Abstraction.Commands;
using Template.Application.Complaints.Dtos;

namespace Template.Application.Complaints.Commands.Proceed;

public class ProceedComplaintCommand : ICommand<ComplaintDto>
{
    public int ComplaintId { get; set; }
}
