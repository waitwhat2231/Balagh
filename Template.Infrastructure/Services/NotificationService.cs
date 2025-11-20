using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Notifications;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;
namespace Template.Infrastructure.Services;

public class NotificationService(TemplateDbContext dbContext, IDeviceRepository deviceRepository) : INotificationService
{
    public async Task<List<GroupedNotification>> GetCurrentUserNotificationsAsync(string userId)
    {
        var grouped = await dbContext.Notifications
            .Include(n => n.Device)
            .Where(n => n.Device.UserId == userId)
            .ToListAsync();

        var result = grouped
            .GroupBy(n => n.CreatedAt!.Value.Date)
            .Select(g => new GroupedNotification
            {
                CreatedAt = g.Key,
                Notifications = g.Select(n => new NotificationDto
                {
                    Id = n.Id,
                    CreatedAt = n.CreatedAt,
                    Title = n.Title,
                    Body = n.Body,
                    DeviceId = n.DeviceId,
                    Read = true
                }).ToList()
            })
            .ToList();

        return result;
    }

    public async Task SaveNotificationAsync(Domain.Entities.Notification entity)
    {
        dbContext.Notifications.Add(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task SendNotificationAsync(Domain.Entities.Notification entity)
    {
        var device = await dbContext.Devices.FindAsync(entity.DeviceId);

        if (device != null && !string.IsNullOrEmpty(device.FcmToken) && device.IsActive == true)
        {
            var message = new Message()
            {
                Notification = new FirebaseAdmin.Messaging.Notification()
                {
                    Title = entity.Title,
                    Body = entity.Body,
                },
                Data = new Dictionary<string, string>()
                {
                    { "title", entity.Title ?? "" },
                    { "body", entity.Body ?? "" },
                    { "createdAt", entity.CreatedAt.ToString() ?? "" },
                    { "type", entity.Type ?? "general" }
                },
                Android = new AndroidConfig
                {
                    Priority = Priority.High,
                    Notification = new AndroidNotification
                    {
                        Sound = "default",
                        ChannelId = "high_importance_channel",
                        ClickAction = "FLUTTER_NOTIFICATION_CLICK"
                    }
                },
                Token = device.FcmToken,
            };
            try
            {
                var messaging = FirebaseMessaging.DefaultInstance;
                var result = await messaging.SendAsync(message);
            }
            catch (FirebaseAdmin.Messaging.FirebaseMessagingException ex)
            {

            }
        }
    }

    public async Task SendTestNotificationAsync(string fcmToken)
    {
        var message = new Message()
        {
            Notification = new FirebaseAdmin.Messaging.Notification()
            {
                Title = "Test notification",
                Body = "This body is for test notification",
            },
            Data = new Dictionary<string, string>()
            {
                { "title", "Test notification" },
                { "body", "Test notification" },
                { "createdAt", DateTime.UtcNow.ToString() },
                { "type", "general" }
            },
            Android = new AndroidConfig
            {
                Priority = Priority.High,
                Notification = new AndroidNotification
                {
                    Sound = "default",
                    ChannelId = "high_importance_channel",
                    ClickAction = "FLUTTER_NOTIFICATION_CLICK"
                }
            },
            Token = fcmToken
        };

        var messaging = FirebaseMessaging.DefaultInstance;
        var result = await messaging.SendAsync(message);

        if (!string.IsNullOrEmpty(result))
        {
            // Message was sent successfully
        }
        else
        {
            // There was an error sending the message
            throw new Exception("Error sending the message.");
        }
    }

    public async Task SaveTestNotification(string fcmToken)
    {
        var notification = new Domain.Entities.Notification()
        {
            Title = "Test notification",
            Body = "This is a test body for test notification",
            CreatedAt = DateTime.Now,
            Read = false
        };

        var deviceExist = await deviceRepository.GetDeviceByToken(fcmToken, null);
        if (deviceExist == null)
        {
            deviceExist = new Domain.Entities.Device()
            {
                FcmToken = fcmToken,
                IsActive = true,
                LastLoggedInAt = DateTime.UtcNow,
                UserId = null
            };

            await deviceRepository.AddAsync(deviceExist);
            await dbContext.SaveChangesAsync();
        }

        deviceExist.Notifications.Add(notification);
        await dbContext.SaveChangesAsync();
    }
}
