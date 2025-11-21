using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Repositories;

namespace Template.Application.Notification.Command.Send;

public class SendNotificationCommandHandler(IMapper mapper, ILogger<SendNotificationCommandHandler> logger,
    INotificationService notificationService) : IRequestHandler<SendNotificationCommand>
{
    public async Task Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Sending Notification");

        var notification = new Domain.Entities.Notification()
        {
            Title = request.Title,
            Body = request.Body,
            DeviceId = request.DeviceId,
            Read = false,
            CreatedAt = DateTime.UtcNow,
        };

        await notificationService.SendNotificationAsync(notification);

        await notificationService.SaveNotificationAsync(notification);
    }
}
