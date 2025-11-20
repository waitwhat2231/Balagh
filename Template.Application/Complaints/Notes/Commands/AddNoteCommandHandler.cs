using Microsoft.Extensions.Logging;
using System.Text.Json;
using Template.Application.Abstraction.Commands;
using Template.Application.Users;
using Template.Domain.Entities;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Complaints.Notes.Commands
{
    public class AddNoteCommandHandler(IComplaintRepository complaintRepository, ILogger<AddNoteCommandHandler> logger,
        INotificationService notificationService, IUserContext userContext,
        IHistoryRepository historyRepository, INotesRepository notesRepository) : ICommandHandler<AddNoteCommand>
    {
        public async Task<Result> Handle(AddNoteCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Adding note to complaint with Id of {request.ComplaintId}");
            var userId = userContext.GetCurrentUser()!.Id;
            var complaint = await complaintRepository.FindByIdAsync(request.ComplaintId);
            if (complaint == null)
            {
                return Result.Failure(["Complaint does not Exist"]);
            }
            var note = new Note()
            {
                UserId = userId,
                ComplaintId = request.ComplaintId,
                CreatedAt = DateTime.UtcNow,
                NoteBody = request.NoteBody,
            };
            var noteEntity = await notesRepository.AddAsync(note);
            var historyRecord = new History()
            {
                ComplaintId = request.ComplaintId,
                ChangeType = Domain.ChangeType.AddNote,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                OldValue = string.Empty,
                NewValue = request.NoteBody,
                ChangeDetails = JsonSerializer.Serialize(new { note_id = noteEntity.Id }),
            };
            await historyRepository.AddAsync(historyRecord);
            return Result.Success();
        }
    }
}
