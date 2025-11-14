using MediatR;

namespace Template.Application.Users.Commands.ForgotPassword;

public class ForgotPasswordCommand : IRequest
{
    public string Email { get; set; } = default!;
}
