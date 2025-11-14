using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Template.Application.Abstraction.Queries;
using Template.Application.Users.Dtos;
using Template.Domain.Entities;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Users.Queries.UserDetails
{
    public class GetUserDetailsByIdQueryHandler(IMapper mapper, IAccountRepository accountRepository,
        UserManager<User> userManager, IUserContext userContext) : IQueryHandler<GetUserDetailsByIdQuery, UserDetailedDto>
    {
        public async Task<Result<UserDetailedDto>> Handle(GetUserDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await accountRepository.GetUserDetails(request.Id);
            if (user == null)
            {
                return Result.Failure<UserDetailedDto>(["Data not Found"]);
            }
            var userRole = await userManager.GetRolesAsync(user);
            var role = userRole.First();
            var userDetails = mapper.Map<UserDetailedDto>(user);
            userDetails.Role = role;
            return Result.Success(userDetails);
        }
    }
}
