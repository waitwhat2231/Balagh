namespace Template.Domain.Entities;

public class ComplaintFile
{
    public int Id { get; set; }
    public string Path { get; set; } = string.Empty;
    public Complaint Complaint { get; set; } = default!;
    public int ComplaintId { get; set; }
    public bool IsDeleted { get; set; }
}
