using Microsoft.Extensions.Logging;
using System.Text.Json;
using Template.Application.Abstraction.Commands;
using Template.Application.Users;
using Template.Domain.Entities;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Complaints.Commands.ExtraInformation
{
    public class RequestExtraInfromationCommandHandler(ILogger<RequestExtraInfromationCommandHandler> logger,
        IHistoryRepository historyRepository, IAccountRepository accountRepository, IComplaintRepository complaintRepository,
        INotificationService notificationService, IUserContext userContext) : ICommandHandler<RequestExtraInfromationCommand>
    {
        public async Task<Result> Handle(RequestExtraInfromationCommand request, CancellationToken cancellationToken)
        {
            var complaint = await complaintRepository.FindByIdAsync(request.ComplaintId);
            if (complaint is null)
            {
                return Result.Failure(["Complaint does not exist"]);
            }
            var currentUser = userContext.GetCurrentUser();
            var sendToNotificationUser = await accountRepository.GetUserWithDevicesAsync(complaint.UserId);
            if (sendToNotificationUser is null)
            {
                return Result.Failure(["User does not Exist"]);
            }
            foreach (var device in sendToNotificationUser.Devices)
            {
                var notification = new Domain.Entities.Notification()
                {
                    Title = "Extra Information Requested",
                    Body = request.Message,
                    DeviceId = device.Id,
                    CreatedAt = DateTime.UtcNow,
                    Read = false,
                    Type = Domain.NotificationType.ExtraInformationRequest
                };
                await notificationService.SaveNotificationAsync(notification);
                await notificationService.SendNotificationAsync(notification);
            }
            History sendNotificationHistoryRecord = new History()
            {
                UserId = currentUser.Id,
                ComplaintId = request.ComplaintId,
                CreatedAt = DateTime.UtcNow,
                ChangeType = Domain.ChangeType.RequestMoreInformation,
                OldValue = string.Empty,
                NewValue = request.Message,
                ChangeDetails = JsonSerializer.Serialize(new { complaint_id = request.ComplaintId })
            };
            await historyRepository.AddAsync(sendNotificationHistoryRecord);
            return Result.Success();
        }
    }
}
