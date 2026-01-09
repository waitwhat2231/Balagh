using MediatR;
using Template.Application.Complaints.Cache;
using Template.Domain.Events;

namespace Template.Application.Events
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task DispatchAsync(IEnumerable<DomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                if (domainEvent is ComplaintUpdatedDomainEvent e)
                {
                    await _mediator.Publish(new DeleteCacheCommand(
                        e.complaintId
                    ));
                }
            }
        }
    }
}
