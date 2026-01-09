using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities;

namespace Template.Application.Complaints.Cache
{
    public class DeleteCacheCommandHandler : INotificationHandler<DeleteCacheCommand>
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<DeleteCacheCommandHandler> _logger;

        public DeleteCacheCommandHandler(IMemoryCache cache, ILogger<DeleteCacheCommandHandler> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public Task Handle(
            DeleteCacheCommand notification,
            CancellationToken cancellationToken)
        {
            var key = $"complaint-{notification.ComplaintId}";
            _logger.LogInformation(key);
            Complaint? comp;
            if (_cache.TryGetValue(key, out comp))
            {
                if (comp != null)
                    _logger.LogInformation(comp.RowVersion.ToString());
                _cache.Remove(key);
            }

            return Task.CompletedTask;
        }
    }
}
