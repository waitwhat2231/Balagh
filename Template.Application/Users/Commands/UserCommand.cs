using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.AuthEntities;

namespace Template.Application.Users.Commands;
public class RegisterUserCommand : ICommand<IEnumerable<IdentityError>>
{
    public string UserName { get; set; }
    [EmailAddress]
    public string Email { get; set; }

    public string Password { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }
    public string Role { get; set; }
}

public class LoginUserCommand : ICommand<AuthResponse?>
{

    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? DeviceToken { get; set; }
}


