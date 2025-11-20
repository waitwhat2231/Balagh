namespace Template.Domain.Entities;

public class Notification
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
    public int? DeviceId { get; set; }
    public Device Device { get; set; }
    public bool Read { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string Type { get; set; } = string.Empty;
}
