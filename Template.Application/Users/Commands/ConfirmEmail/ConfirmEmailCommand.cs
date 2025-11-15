using Template.Application.Abstraction.Commands;

namespace Template.Application.Users.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : ICommand
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
