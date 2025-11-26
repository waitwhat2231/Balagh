using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;
using Template.Domain.Services;

namespace Template.Application.Complaints.Commands.AddFile;

public class AddFileCommandHandler(ILogger<AddFileCommandHandler> logger, IComplaintRepository complaintRepository,
    IFileService fileService)
    : ICommandHandler<AddFileCommand>
{
    public async Task<Result> Handle(AddFileCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding new file");

        var dbComplaint = await complaintRepository.GetComplaintByIdWithFilesAsync(request.ComplaintId);
        if (dbComplaint == null)
        {
            throw new NotFoundException("Complain", request.ComplaintId.ToString());
        }

        var newFilePath = await fileService.SaveFileAsync(request.NewFile, "Uploads/Complaints", [".jpg", ".png", ".pdf"]);
        dbComplaint.ComplaintFiles.Add(new ComplaintFile
        {
            IsDeleted = false,
            Path = newFilePath,
            ComplaintId = dbComplaint.Id,
        });

        await complaintRepository.SaveChangesAsync();
        return Result.Success();
    }
}
