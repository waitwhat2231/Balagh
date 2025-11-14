namespace Template.Domain.Entities;

public class Notification
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public Device Device { get; set; } = default!;
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
