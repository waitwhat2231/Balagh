namespace Template.Domain.Events
{
    public record ComplaintUpdatedDomainEvent(int complaintId) : DomainEvent;
}
