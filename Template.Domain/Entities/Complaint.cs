using System.ComponentModel.DataAnnotations;

namespace Template.Domain.Entities;

public class Complaint
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public User User { get; set; } = default!;
    public int GovernmentalEntityId { get; set; }
    public GovernmentalEntity GovernmentalEntity { get; set; } = default!;

    public string Location { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ComplaintStatus Status { get; set; }
    public bool IsLocked { get; set; }
    public bool IsDeleted { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = [];
    public List<ComplaintFile> ComplaintFiles { get; set; } = [];
    public List<Note> Notes { get; set; } = [];
    public List<History> Histories { get; set; } = [];
}
