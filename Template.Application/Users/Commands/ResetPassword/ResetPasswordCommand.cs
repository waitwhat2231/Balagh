using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Template.Application.Users.Commands.ResetPassword;

public class ResetPasswordCommand : IRequest<IEnumerable<IdentityError>>
{
    [Required]
    public string NewPassword { get; set; } = default!;
    [Required]
    [Compare("NewPassword", ErrorMessage = "Password do not match")]
    public string ConfirmNewPassword { get; set; } = default!;
    public string Token { get; set; } = default!;
}
