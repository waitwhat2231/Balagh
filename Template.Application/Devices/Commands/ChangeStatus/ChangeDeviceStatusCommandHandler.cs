using MediatR;
using Template.Domain.Repositories;

namespace Template.Application.Devices.Commands.ChangeStatus;

public class ChangeDeviceStatusCommandHandler(IDeviceRepository deviceRepository) : IRequestHandler<ChangeDeviceStatusCommand>
{
    public async Task Handle(ChangeDeviceStatusCommand request, CancellationToken cancellationToken)
    {
        var device = await deviceRepository.GetDeviceByToken(request.DeviceToken, null);
        if (device == null)
        {
            return;
        }

        device.IsActive = request.OptIn;
        // await deviceRepository.UpdateAsync(device);
        await deviceRepository.SaveChangesAsync();
    }
}
