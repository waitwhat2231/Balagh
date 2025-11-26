using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Application.Complaints.Commands.AddFile;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;
using Template.Domain.Services;

namespace Template.Application.Complaints.Commands.DeleteFile;

public class DeleteFileCommandHandler(ILogger<AddFileCommandHandler> logger, IComplaintRepository complaintRepository,
    IFileService fileService) : ICommandHandler<DeleteFileCommand>
{
    public async Task<Result> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding new file");

        var dbComplaint = await complaintRepository.GetComplaintByIdWithFilesAsync(request.ComplaintId);
        if (dbComplaint == null)
        {
            throw new NotFoundException("Complain", request.ComplaintId.ToString());
        }

        var existingFile = dbComplaint.ComplaintFiles.FirstOrDefault(f => f.Id == request.FileId);
        if (existingFile == null)
        {
            throw new NotFoundException("Complaint File", request.FileId.ToString());
        }
        fileService.DeleteFile(existingFile.Path);
        dbComplaint.ComplaintFiles.Remove(existingFile);

        await complaintRepository.SaveChangesAsync();
        return Result.Success();
    }
}
