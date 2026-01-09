using Template.Application.Abstraction.Commands;
using Template.Domain.AuthEntities;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.ConfirmEmail
{
    class ConfirmEmailWithLoginCommandHandler(IAccountRepository accountRepository) : ICommandHandler<ConfirmEmailWithLoginCommand, AuthResponse?>
    {
        public async Task<Result<AuthResponse?>> Handle(ConfirmEmailWithLoginCommand request, CancellationToken cancellationToken)
        {
            var tokens = await accountRepository.ConfirmEmailAndLoginAsync(request.Email, request.Code, request.DeviceToken);
            if (tokens is not null)
            {
                return Result.Success(tokens)!;
            }
            return Result.Failure<AuthResponse?>(["Code to confirm Email is wrong"]);
        }
    }
}
