using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Queries;
using Template.Application.Complaints.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Complaints.Queries.GetById;

public class GetComplaintByIdQueryHandler(ILogger<GetComplaintByIdQueryHandler> logger, IComplaintRepository complaintRepository,
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
        return Result<ComplaintDto>.Success(result);
    }
}
