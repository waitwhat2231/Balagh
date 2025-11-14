using MediatR;

namespace Template.Application.Users.Commands.DeleteById;

public class DeleteUserAccountByIdCommand : IRequest
{
    public string UserId { get; set; } = string.Empty;
}
