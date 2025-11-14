/*using MediatR;
using Template.Domain.Entities;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler(IAccountRepository accountRepository)
    : IRequestHandler<ForgotPasswordCommand>
{
 *//*   public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await accountRepository.FindUserByEmail(request.Email);

        if (existingUser == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }

        try
        {
            existingUser.Otp = new Random().Next(100000, 999999).ToString();
            existingUser.ExpiryOtpDate = DateTime.UtcNow.AddMinutes(10);
            await accountRepository.UpdateUser(existingUser);

            await accountRepository.SendEmail(request.Email, existingUser.Otp);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }*//*
}
*/