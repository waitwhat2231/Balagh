using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Application.Complaints.Dtos;
using Template.Application.Users;
using Template.Domain.Entities;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;
using Template.Domain.Services;

namespace Template.Application.Complaints.Commands.Create;

public class CreateComplaintCommandHandler(ILogger<CreateComplaintCommandHandler> logger, IComplaintRepository complaintRepository,
    IUserContext userContext, IMapper mapper, IFileService fileService)
    : ICommandHandler<CreateComplaintCommand, ComplaintDto>
{
    public async Task<Result<ComplaintDto>> Handle(CreateComplaintCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new complaint");

        string currentUserId = userContext.GetCurrentUser()!.Id;
        var complaint = mapper.Map<Complaint>(request);
        complaint.UserId = currentUserId;
        complaint.Status = Domain.ComplaintStatus.New;

        if (request.ComplaintFiles.Count > 0)
        {
            var savedPaths = await fileService.SaveFilesAsync(request.ComplaintFiles, "Uploads/Complaints/", [".jpg", ".png", ".pdf", ".jpeg"]);
            foreach (var filePath in savedPaths)
            {
                complaint.ComplaintFiles.Add(new ComplaintFile
                {
                    IsDeleted = false,
                    Path = filePath,
                });
            }
        }

        var created = await complaintRepository.AddAsync(complaint);
        var result = mapper.Map<ComplaintDto>(created);
        return Result<ComplaintDto>.Success(result);
    }
}
