using Template.Domain.Entities;
using Template.Domain.Entities.Notifications;
namespace Template.Domain.Repositories;

public interface INotificationService
{
    public Task SaveNotificationAsync(Notification entity);
    public Task SendNotificationAsync(Notification entity);
    public Task<List<GroupedNotification>> GetCurrentUserNotificationsAsync(string userId);
    public Task SendTestNotificationAsync(string fcmToken);
    public Task SaveTestNotification(string fcmToken);
}

