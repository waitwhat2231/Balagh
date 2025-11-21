using AutoMapper;
using MediatR;
using Template.Application.Users;
using Template.Domain.Entities.Notifications;
using Template.Domain.Repositories;

namespace Template.Application.Notification.Queries.CurrentUserNotifications;

public class GetCurrentUserNotificationsQueryHandler(INotificationService notificationService, IMapper mapper,
    IUserContext userContext)
    : IRequestHandler<GetCurrentUserNotificationsQuery, IEnumerable<GroupedNotification>>
{
    public async Task<IEnumerable<GroupedNotification>> Handle(GetCurrentUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = userContext.GetCurrentUser()!.Id;
        var notifications = await notificationService.GetCurrentUserNotificationsAsync(currentUserId);
        return notifications;
    }
}
