using MediatR;

namespace Template.Application.Notification.Command.Send;

public class SendNotificationCommand : IRequest
{
    public string Title { get; set; } = default!;
    public string Body { get; set; } = default!;
    public int DeviceId { get; set; }
}
