using System.ComponentModel.DataAnnotations;
using Template.Application.Abstraction.Commands;
using Template.Domain.AuthEntities;
namespace Template.Application.Users.Commands.ChangePassword;

public class ChangePasswordCommand : ICommand<AuthResponse>
{
    [Required]
    public string NewPassword { get; set; } = default!;
    [Required]
    [Compare("NewPassword", ErrorMessage = "Password do not match")]
    public string ConfirmNewPassword { get; set; } = default!;
    [Required]
    public string OldPassword { get; set; } = default!;
}
