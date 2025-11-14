using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Users.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Users.Queries.CurrentUser;

class GetCurrentUserQueryHandler(ILogger<GetCurrentUserQueryHandler> logger, IMapper mapper, IUserContext userContext, IAccountRepository accountRepository) : IRequestHandler<GetCurrentUserQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var current_User = userContext.GetCurrentUser();
        var fullUser = await accountRepository.GetUserAsync(current_User.Id, current_User.Roles.Contains("AssistantDoctor"));
        if (fullUser == null)
        {
            return Result.Failure<UserDto>(["User not found"]);
        }
        var response = mapper.Map<UserDto>(fullUser);
        response.Role = current_User.Roles.First();
        return Result.Success(response);
    }
}
