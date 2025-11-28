using Microsoft.Extensions.Logging;
using System.Text.Json;
using Template.Application.Abstraction.Commands;
using Template.Application.Complaints.Commands.AddFile;
using Template.Application.Users;
using Template.Domain;
using Template.Domain.Entities;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;
using Template.Domain.Services;

namespace Template.Application.Complaints.Commands.DeleteFile;

public class DeleteFileCommandHandler(ILogger<AddFileCommandHandler> logger, IComplaintRepository complaintRepository, IUserContext userContext,
    IFileService fileService) : ICommandHandler<DeleteFileCommand>
{
    public async Task<Result> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding new file");
        var currentUser = userContext.GetCurrentUser();
        var dbComplaint = await complaintRepository.GetComplaintByIdWithFilesAsync(request.ComplaintId);
        if (dbComplaint == null)
        {
            throw new NotFoundException("Complain", request.ComplaintId.ToString());
        }
        if (dbComplaint.UserId != currentUser.Id && dbComplaint.LockedBy != currentUser.Id)
        {
            throw new ForbiddenException("Editing this Complaint");
        }
        var existingFile = dbComplaint.ComplaintFiles.FirstOrDefault(f => f.Id == request.FileId);
        if (existingFile == null)
        {
            throw new NotFoundException("Complaint File", request.FileId.ToString());
        }
        fileService.DeleteFile(existingFile.Path);
        dbComplaint.ComplaintFiles.Remove(existingFile);
        AddHistory(dbComplaint.Id, currentUser.Id, dbComplaint.Histories, ChangeType.DeleteFile, existingFile.Path, "");
        await complaintRepository.SaveChangesAsync();
        return Result.Success();
    }
    private static void AddHistory(int complaintId, string userId, List<History> historyEntries, ChangeType type, object oldValue, object newValue, object? details = null)
    {
        historyEntries.Add(new History
        {
            ComplaintId = complaintId,
            UserId = userId,
            ChangeType = type,
            OldValue = JsonSerializer.Serialize(oldValue),
            NewValue = JsonSerializer.Serialize(newValue),
            ChangeDetails = details != null ? JsonSerializer.Serialize(details) : string.Empty,
            CreatedAt = DateTime.UtcNow
        });
    }
}
