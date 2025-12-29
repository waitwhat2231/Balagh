using Template.Domain;

namespace Template.Application.Reports.Dtos;

public class ComplaintsStatusReportCountDto
{
    public ComplaintStatus Status { get; set; }
    public int Count { get; set; }

}
