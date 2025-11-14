using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Queries;
using Template.Application.Complaints.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Complaints.Queries.GetAll;

public class GetAllComplaintsQueryHandler(ILogger<GetAllComplaintsQueryHandler> logger, IMapper mapper,
    IComplaintRepository complaintRepository) : IQueryHandler<GetAllComplaintsQuery, IEnumerable<ComplaintDto>>
{
    public async Task<Result<IEnumerable<ComplaintDto>>> Handle(GetAllComplaintsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all complaints");
        var complaints = await complaintRepository.GetAllAsync();
        var result = mapper.Map<IEnumerable<ComplaintDto>>(complaints);
        return Result<IEnumerable<ComplaintDto>>.Success(result);
    }
}
