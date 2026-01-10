using MediatR;
using System.Diagnostics;
using Template.Application.Complaints.Cache;
using Template.Domain.Events;

namespace Template.Application.Events
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        private readonly ActivitySource _activitySource;

        public DomainEventDispatcher(IMediator mediator, ActivitySource activitySource)
        {
            _mediator = mediator;
            _activitySource = activitySource;
        }

        public async Task DispatchAsync(IEnumerable<DomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                using var activity = _activitySource.StartActivity(domainEvent.GetType().Name);
                activity?.SetTag("domain.event.type", domainEvent.GetType().Name);
                try
                {
                    if (domainEvent is ComplaintUpdatedDomainEvent e)
                    {
                        await _mediator.Publish(new DeleteCacheCommand(
                            e.complaintId
                        ));
                    }
                    activity?.SetTag("status", "success");
                }
                catch (Exception ex)
                {
                    activity?.AddException(ex);
                    activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                    throw;
                }

            }
        }
    }
}
