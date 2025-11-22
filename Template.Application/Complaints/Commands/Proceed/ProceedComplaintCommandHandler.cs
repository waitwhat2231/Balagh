using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Application.Complaints.Dtos;
using Template.Application.Users;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Complaints.Commands.Proceed;

public class ProceedComplaintCommandHandler(ILogger<ProceedComplaintCommandHandler> logger, IComplaintRepository complaintRepository,
    IMapper mapper, IUserContext userContext, IAccountRepository accountRepository) : ICommandHandler<ProceedComplaintCommand, ComplaintDto>
{
    public async Task<Result<ComplaintDto>> Handle(ProceedComplaintCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Attempting to Lock complaint with Id {request.ComplaintId}");
        var currentUserId = userContext.GetCurrentUser()!.Id;
        var dbUser = await accountRepository.FindUserById(currentUserId);

        var existingComplaint = await complaintRepository.GetComplaintByIdWithFilesAsync(request.ComplaintId);
        if (existingComplaint == null)
        {
            return Result<ComplaintDto>.Failure(new List<string> { "Complaint not found " });
        }

        existingComplaint.IsLocked = true;
        existingComplaint.LockedBy = currentUserId;
        await complaintRepository.UpdateAsync(existingComplaint);
        //await complaintRepository.SaveChangesAsync();

        var result = mapper.Map<ComplaintDto>(existingComplaint);
        return Result<ComplaintDto>.Success(result);
    }
}
