using Template.Domain;

namespace Template.Application.Reports.Dtos;

public class ComplaintsStatusReportCountDto
{
    public ComplaintStatus Status { get; set; }
    public int ComplaintCount { get; set; }
    public int PercentageOfTotalComplaints { get; set; }

}
