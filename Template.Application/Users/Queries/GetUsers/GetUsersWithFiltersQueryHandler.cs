using AutoMapper;
using Template.Application.Abstraction.Queries;
using Template.Application.Users.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Users.Queries.GetUsers
{
    public class GetUsersWithFiltersQueryHandler(IAccountRepository accountRepository, IMapper mapper) : IQueryHandler<GetUsersWithFiltersQuery, List<UserDto>>
    {
        public async Task<Result<List<UserDto>>> Handle(GetUsersWithFiltersQuery request, CancellationToken cancellationToken)
        {
            var users = await accountRepository.GetUsersWithFilters(request.Role,
                request.Email, request.PhoneNumber, request.ClinicAddress, request.ClinicName);
            if (users == null)
            {
                return Result.Failure<List<UserDto>>(["Entity not found"]);
            }
            var userDtos = mapper.Map<List<UserDto>>(users);
            return Result.Success(userDtos);

        }
    }
}
