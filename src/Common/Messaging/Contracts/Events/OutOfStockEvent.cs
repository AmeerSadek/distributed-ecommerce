namespace Common.Messaging.Contracts.Events;

public record OutOfStockEvent(Guid OrderId, Guid ProductId, int Quantity);
