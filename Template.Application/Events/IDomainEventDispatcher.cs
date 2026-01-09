using Template.Domain.Events;

namespace Template.Application.Events
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(IEnumerable<DomainEvent> domainEvents);
    }
}
