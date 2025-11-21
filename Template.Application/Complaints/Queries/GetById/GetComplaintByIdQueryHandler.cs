using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Queries;
using Template.Application.Complaints.Dtos;
using Template.Application.Complaints.Notes.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Complaints.Queries.GetById;

public class GetComplaintByIdQueryHandler(ILogger<GetComplaintByIdQueryHandler> logger, IComplaintRepository complaintRepository,
    IAccountRepository accountRepository, INotesRepository notesRepository,
    IMapper mapper) : IQueryHandler<GetComplaintByIdQuery, ComplaintDto>
{
    public async Task<Result<ComplaintDto>> Handle(GetComplaintByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting complaint by id");

        var complaint = await complaintRepository.GetComplaintByIdWithFilesAsync(request.ComplaintId);
        if (complaint == null)
        {
            throw new InvalidOperationException("Complaint not found");
        }
        var result = mapper.Map<ComplaintDto>(complaint);
        var user = await accountRepository.GetUserAsync(complaint.UserId);
        result.UserName = user.UserName;
        if (request.IncludeNotes == true)
        {
            var notes = await notesRepository.GetNotesFromComplaintId(request.ComplaintId);
            result.Notes = mapper.Map<List<NoteDto>>(notes);
        }
        return Result<ComplaintDto>.Success(result);
    }
}
