namespace Template.Domain.Entities;

public class History
{
    public int Id { get; set; }
    public int ComplaintId { get; set; }
    public Complaint Complaint { get; set; } = default!;
    public User User { get; set; } = default!;
    public string UserId { get; set; } = string.Empty; // ChangedBy
    public ChangeType ChangeType { get; set; }
    public string OldValue { get; set; } = string.Empty; //Json
    public string NewValue { get; set; } = string.Empty; //Json
    public string ChangeDetails { get; set; } = string.Empty; //Json
    public DateTime CreatedAt { get; set; }
}
