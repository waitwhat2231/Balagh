using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Domain.AuthEntities;
using Template.Domain.Entities;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Enums;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands;

public class RegisterUserCommandHandler(IMapper mapper,
        IUserContext userContext,
        ILogger<RegisterUserCommandHandler> logger,
        IGovermentalEntitiesRepository govermentalEntitiesRepository,
        IAccountRepository accountRepository
        ) : ICommandHandler<RegisterUserCommand, IEnumerable<IdentityError>>
{
    public async Task<Result<IEnumerable<IdentityError>>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request);
        var currentUser = userContext.GetCurrentUser();
        var currentUserRole = currentUser != null ? currentUser.Roles.FirstOrDefault() : "NoRole";
        if ((request.Role.ToUpper() == nameof(EnumRoleNames.Administrator).ToUpper()) ||
            (request.Role.ToUpper() == nameof(EnumRoleNames.Employee).ToUpper() &&
            currentUserRole.ToUpper() != nameof(EnumRoleNames.Administrator).ToUpper()))
        {
            throw new ForbiddenException("Registering account of this type ");
        }
        user.UserName = request.UserName;
        if (request.GovernmentalEntityId is not null)
        {
            var govEnt = await govermentalEntitiesRepository.FindByIdAsync((int)request.GovernmentalEntityId);
            if (govEnt == null)
            {
                return Result.Failure<IEnumerable<IdentityError>>(["Govermental Entity Does not Exist"]);
            }
        }
        var errors = await accountRepository.Register(user, request.Password, request.Role);
        return Result.Success(errors);
    }
}
public class LoginUserCommandHandler(ILogger<LoginUserCommandHandler> logger,
        ITokenRepository tokenRepository,
        IAccountRepository accountRepository,
        UserManager<User> userManager, IDeviceRepository deviceRepository) : ICommandHandler<LoginUserCommand, AuthResponse?>
{
    public async Task<Result<AuthResponse?>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("looking for user with email: {Email}", request.Email);
        var tokenResponse = await accountRepository.LoginUser(request.Email, request.Password, request.DeviceToken);
        if (tokenResponse != null)
        {
            return Result.Success(tokenResponse);
        }
        return Result.Failure<AuthResponse?>(["Email or Password is Wrong"]);
    }
}
