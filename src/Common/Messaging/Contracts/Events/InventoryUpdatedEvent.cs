namespace Common.Messaging.Contracts.Events;

public record InventoryUpdatedEvent(Guid OrderId, Guid ProductId, int Quantity);
