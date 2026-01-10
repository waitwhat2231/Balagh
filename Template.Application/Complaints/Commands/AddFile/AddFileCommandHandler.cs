using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Application.Events;
using Template.Application.Helper;
using Template.Application.Users;
using Template.Domain;
using Template.Domain.Entities;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;
using Template.Domain.Services;

namespace Template.Application.Complaints.Commands.AddFile;

public class AddFileCommandHandler(ILogger<AddFileCommandHandler> logger, IComplaintRepository complaintRepository,
    IUserContext userContext,
    IDomainEventDispatcher domainEventDispatcher,
    IFileService fileService)
    : ICommandHandler<AddFileCommand>
{
    public async Task<Result> Handle(AddFileCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding new file");
        var currentUser = userContext.GetCurrentUser();
        var dbComplaint = await complaintRepository.GetComplaintByIdWithFilesAsync(request.ComplaintId);

        if (dbComplaint == null)
        {
            throw new NotFoundException("Complain", request.ComplaintId.ToString());
        }
        if (dbComplaint.UserId != currentUser.Id)
        {
            throw new ForbiddenException("Editing this Complaint");
        }
        complaintRepository.ApplyConcurrencyCheck(dbComplaint, request.RowVersion);
        var newFilePath = await fileService.SaveFileAsync(request.NewFile, "Uploads/Complaints", [".jpg", ".png", ".pdf"]);
        dbComplaint.ComplaintFiles.Add(new ComplaintFile
        {
            IsDeleted = false,
            Path = newFilePath,
            ComplaintId = dbComplaint.Id,
        });
        AddHistory(dbComplaint.Id, currentUser.Id, dbComplaint.Histories, ChangeType.AddFile, "", request.NewFile.FileName);
        await complaintRepository.SaveChangesAsync();
        dbComplaint.Update();
        await domainEventDispatcher.DispatchAsync(dbComplaint.DomainEvents);
        dbComplaint.ClearDomainEvents();
        return Result.Success();
    }
    private static void AddHistory(int complaintId, string userId, List<History> historyEntries, ChangeType type, object oldValue, object newValue, object? details = null)
    {
        historyEntries.Add(new History
        {
            ComplaintId = complaintId,
            UserId = userId,
            ChangeType = type,
            OldValue = JsonHelper.Serialize(oldValue),
            NewValue = JsonHelper.Serialize(newValue),
            ChangeDetails = details != null ? JsonHelper.Serialize(details) : string.Empty,
            CreatedAt = DateTime.UtcNow
        });
    }
}
