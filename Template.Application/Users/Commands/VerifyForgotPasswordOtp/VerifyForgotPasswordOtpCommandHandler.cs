using MediatR;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.VerifyForgotPasswordOtp;

public class VerifyForgotPasswordOtpCommandHandler(IAccountRepository accountRepository)
    : IRequestHandler<VerifyForgotPasswordOtpCommand, (bool IsValid, string Token)>
{
    public async Task<(bool IsValid, string Token)> Handle(VerifyForgotPasswordOtpCommand request, CancellationToken cancellationToken)
    {
        return await accountRepository.VerifyForgotPasswordOtp(request.Code);
    }
}
