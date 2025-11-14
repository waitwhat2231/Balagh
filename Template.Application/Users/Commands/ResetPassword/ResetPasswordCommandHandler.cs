using MediatR;
using Microsoft.AspNetCore.Identity;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.ResetPassword;

public class ResetPasswordCommandHandler(IAccountRepository accountRepository) : IRequestHandler<ResetPasswordCommand, IEnumerable<IdentityError>>
{
    public async Task<IEnumerable<IdentityError>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var errors = await accountRepository.ResetPassword(request.Token, request.NewPassword);
        return errors;

    }
}
