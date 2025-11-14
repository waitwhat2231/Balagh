using MediatR;

namespace Template.Application.Users.Commands.VerifyForgotPasswordOtp;

public class VerifyForgotPasswordOtpCommand : IRequest<(bool IsValid, string Token)>
{
    public string Code { get; set; } = default!;
}
