using MediatR;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.DeleteAccount;

public class DeleteAccountCommandHandler(IUserContext userContext, IAccountRepository accountRepository)
    : IRequestHandler<DeleteAccountCommand>
{
    public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = userContext.GetCurrentUser()!.Id;

        if (currentUserId == null)
        {
            throw new UnauthorizedAccessException();
        }

        await accountRepository.DeleteAccount(currentUserId);
    }
}
