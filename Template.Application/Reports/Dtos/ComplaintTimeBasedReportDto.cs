namespace Template.Application.Reports.Dtos
{
    public class ComplaintTimeBasedReportDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int ComplaintCount { get; set; }
        public int PercentageOfTotalComplaints { get; set; }
    }
}
