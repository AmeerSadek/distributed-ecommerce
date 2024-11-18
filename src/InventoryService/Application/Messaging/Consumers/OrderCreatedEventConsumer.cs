using Common.Messaging.Contracts.Events;
using InventoryService.Application.Contracts;
using MassTransit;

namespace InventoryService.Application.Messaging.Consumers;

internal class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly IInventoryService _inventoryService;

    public OrderCreatedEventConsumer(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        await _inventoryService.CheckAndReduceStockAsync(context.Message.ProductId, context.Message.Quantity);
    }
}
