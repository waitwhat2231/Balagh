using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Queries;
using Template.Application.Complaints.Dtos;
using Template.Application.Users;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Enums;
using Template.Domain.Pagination;
using Template.Domain.Repositories;

namespace Template.Application.Complaints.Queries.GetAll;

public class GetAllComplaintsQueryHandler(ILogger<GetAllComplaintsQueryHandler> logger, IMapper mapper,
    IUserContext userContext,
    IComplaintRepository complaintRepository) : IQueryHandler<GetAllComplaintsQuery, PagedEntity<ComplaintDto>>
{
    public async Task<Result<PagedEntity<ComplaintDto>>> Handle(GetAllComplaintsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all complaints");
        var user = userContext.GetCurrentUser();
        var userId = user.Id;
        var userRole = (EnumRoleNames)Enum.Parse(typeof(EnumRoleNames), user.Roles.First());
        var complaints = await complaintRepository.GetAllComplaintsWithUserName(request.PageNum, request.PageSize, userRole, userId);
        var result = new PagedEntity<ComplaintDto>()
        {
            Items = mapper.Map<List<ComplaintDto>>(complaints.Items),
            PageNumber = request.PageNum,
            PageSize = request.PageSize,
            TotalItems = complaints.TotalItems,
        };
        return Result<PagedEntity<ComplaintDto>>.Success(result);
    }
}
