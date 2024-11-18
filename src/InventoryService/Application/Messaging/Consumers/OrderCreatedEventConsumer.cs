using Common.Messaging.Contracts.Events;
using InventoryService.Application.Contracts.Dtos;
using InventoryService.Application.Contracts.Interfaces;
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
        await _inventoryService.CheckAndReduceStockAsync(new CheckAndReduceStockDto
        {
            OrderId = context.Message.OrderId,
            ProductId = context.Message.ProductId,
            Quantity = context.Message.Quantity
        },
        context.CancellationToken);
    }
}
