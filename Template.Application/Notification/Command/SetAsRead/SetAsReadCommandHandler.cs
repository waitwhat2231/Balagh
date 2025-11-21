using MediatR;

namespace Template.Application.Notification.Command.SetAsRead;

public class SetAsReadCommandHandler : IRequestHandler<SetAsReadCommand>
{
    public Task Handle(SetAsReadCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
