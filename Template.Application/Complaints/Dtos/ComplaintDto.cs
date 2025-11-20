using Template.Domain;

namespace Template.Application.Complaints.Dtos;

public class ComplaintDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int GovernmentalEntityId { get; set; }
    public string Location { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ComplaintStatus Status { get; set; }
    public string LockedBy { get; set; } = string.Empty;
    public bool IsLocked { get; set; }
    public List<ComplaintFileDto> ComplaintFiles { get; set; } = [];
    public List<HistoryDto> Histories { get; set; } = [];
}
