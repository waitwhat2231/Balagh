using Template.Domain;

namespace Template.Application.Complaints.Dtos;

public class HistoryDto
{
    public int Id { get; set; }
    public int ComplaintId { get; set; }
    public string UserId { get; set; } = string.Empty; // ChangedBy
    public string UserName { get; set; } = string.Empty;
    public ChangeType ChangeType { get; set; }
    public string OldValue { get; set; } = string.Empty; //Json
    public string NewValue { get; set; } = string.Empty; //Json
    public string ChangeDetails { get; set; } = string.Empty; //Json
    public DateTime CreatedAt { get; set; }
}
