using Solar.Heliac.Domain.Common.Base;

namespace Solar.Heliac.Domain.Common.Interfaces;

public interface IAggregateRoot
{
    public IReadOnlyList<DomainEvent> DomainEvents { get; }

    public void AddDomainEvent(DomainEvent domainEvent);

    public void RemoveDomainEvent(DomainEvent domainEvent);

    public void ClearDomainEvents();
}