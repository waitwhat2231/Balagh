using Template.Application.Abstraction.Commands;
using Template.Domain.AuthEntities;

namespace Template.Application.Users.Commands.ConfirmEmail
{
    public class ConfirmEmailWithLoginCommand : ICommand<AuthResponse?>
    {
        public required string Email { get; set; }
        public required string Code { get; set; }
        public required string DeviceToken { get; set; }
    }
}
