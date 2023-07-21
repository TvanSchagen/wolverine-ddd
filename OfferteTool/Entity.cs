namespace OfferteTool;

public abstract class Entity
{
    private readonly List<object> _domainEvents = new();

    protected void AddDomainEvent(object @event)
    {
        _domainEvents.Add(@event);
    }

    public List<object> GetDomainEvents() => _domainEvents;
}