using Template.Domain;

namespace Template.Application.Reports.Queries
{
    public class GetComplaintReportQueryBase(DateTime from, DateTime to, int? govermentalEntityId = null, string? location = null, ComplaintStatus? status = null)
    {
        public DateTime From { get; set; } = from;
        public DateTime To { get; set; } = to;
        public int? GovermentalEntityId { get; set; } = govermentalEntityId;
        public string? Location { get; set; } = location;
        public ComplaintStatus? Status { get; set; } = status;
    }
}
