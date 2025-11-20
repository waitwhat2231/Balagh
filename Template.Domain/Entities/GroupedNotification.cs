namespace Template.Domain.Entities.Notifications
{
    public class GroupedNotification
    {
        public DateTime? CreatedAt { get; set; }
        public List<NotificationDto> Notifications { get; set; } = new();
    }
}
