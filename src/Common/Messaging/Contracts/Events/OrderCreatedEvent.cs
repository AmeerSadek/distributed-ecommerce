namespace Common.Messaging.Contracts.Events;

public record OrderCreatedEvent
{
    public Guid OrderId { get; init; }

    public Guid ProductId { get; init; }

    public int Quantity { get; init; }
}
