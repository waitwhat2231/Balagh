using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler(IAccountRepository accountRepository) : ICommandHandler<ConfirmEmailCommand>
    {
        public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var emailConfirmed = await accountRepository.ConfirmEmailAsync(request.Email, request.Code);
            if (emailConfirmed)
            {
                return Result.Success();
            }
            return Result.Failure(["Code to confirm Email is wrong"]);
        }
    }
}
