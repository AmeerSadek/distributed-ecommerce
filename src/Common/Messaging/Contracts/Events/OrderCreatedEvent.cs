namespace Common.Messaging.Contracts.Events;

public record OrderCreatedEvent(Guid OrderId, Guid ProductId, int Quantity);
