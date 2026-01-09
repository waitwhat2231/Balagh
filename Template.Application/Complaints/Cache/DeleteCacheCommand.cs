using MediatR;

namespace Template.Application.Complaints.Cache
{
    public record DeleteCacheCommand(int ComplaintId) : INotification;
}
