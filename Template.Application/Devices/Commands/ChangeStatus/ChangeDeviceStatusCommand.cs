using MediatR;
namespace Template.Application.Devices.Commands.ChangeStatus;

public class ChangeDeviceStatusCommand : IRequest
{
    public string DeviceToken { get; set; } = default!;
    public bool OptIn { get; set; }
}
