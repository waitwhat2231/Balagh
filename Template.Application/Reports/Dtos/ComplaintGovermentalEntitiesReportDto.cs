namespace Template.Application.Reports.Dtos
{
    public class ComplaintGovermentalEntitiesReportDto
    {
        public int GovermentalEntityId { get; set; }
        public string GovermentalEntityName { get; set; } = string.Empty;
        public int ComplaintCount { get; set; }
        public int PercentageOfTotalComplaints { get; set; }
    }
}
