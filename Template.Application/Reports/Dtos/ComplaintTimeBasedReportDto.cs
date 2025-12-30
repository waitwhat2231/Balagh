namespace Template.Application.Reports.Dtos
{
    public class ComplaintTimeBasedReportDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int ComplaintCount { get; set; }
        public decimal PercentageOfTotalComplaints { get; set; }
    }
}
