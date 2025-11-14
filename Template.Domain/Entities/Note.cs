namespace Template.Domain.Entities;

public class Note
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public User User { get; set; } = default!;
    public Complaint Complaint { get; set; } = default!;
    public int ComplaintId { get; set; }
    public string NoteBody { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
