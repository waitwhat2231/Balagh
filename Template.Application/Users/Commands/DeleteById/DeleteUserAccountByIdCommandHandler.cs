using MediatR;
using Template.Domain.Entities;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.DeleteById;

public class DeleteUserAccountByIdCommandHandler(IAccountRepository accountRepository)
    : IRequestHandler<DeleteUserAccountByIdCommand>
{
    public async Task Handle(DeleteUserAccountByIdCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await accountRepository.FindUserById(request.UserId);
        if (existingUser == null)
        {
            throw new NotFoundException(nameof(User), request.UserId);
        }
        await accountRepository.DeleteUser(existingUser);
    }
}
