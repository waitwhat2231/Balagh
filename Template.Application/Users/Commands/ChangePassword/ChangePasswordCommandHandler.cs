using Template.Application.Abstraction.Commands;
using Template.Domain.AuthEntities;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.ChangePassword;

public class ChangePasswordCommandHandler(IAccountRepository accountRepository, IUserContext userContext)
    : ICommandHandler<ChangePasswordCommand, AuthResponse>
{
    public async Task<Result<AuthResponse>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        var existingUser = await accountRepository.GetUserDetails(user.Id);
        if (existingUser == null)
        {
            return Result.Failure<AuthResponse>(["User doesn't exist"]);
        }

        var changeResult = await accountRepository.UpdatePassword(existingUser, request.OldPassword, request.NewPassword);
        if (changeResult.Succeeded)
        {
            var token = await accountRepository.LoginUserWithoutDevice(existingUser.Email, request.NewPassword);
            return Result.Success(token);
        }
        return Result.Failure<AuthResponse>(changeResult.Errors.Select(x => x.Code).ToList());

    }
}
