using MediatR;
using Template.Domain.Entities.Notifications;

namespace Template.Application.Notification.Queries.CurrentUserNotifications;

public class GetCurrentUserNotificationsQuery : IRequest<IEnumerable<GroupedNotification>>
{
}
