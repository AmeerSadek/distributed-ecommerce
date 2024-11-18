using Common.Messaging.Contracts.Events;
using InventoryService.Application.Contracts.Dtos;
using InventoryService.Application.Contracts.Interfaces;
using InventoryService.Exceptions;
using MassTransit;

namespace InventoryService.Application.Messaging.Consumers;

internal class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly IInventoryService _inventoryService;
    private readonly ILogger<OrderCreatedEventConsumer> _logger;

    public OrderCreatedEventConsumer(
        IInventoryService inventoryService,
        ILogger<OrderCreatedEventConsumer> logger)
    {
        _inventoryService = inventoryService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        // Demonstrating retires
        if (context.GetRetryAttempt() == 0)
        {
            _logger.LogInformation("First time submitted");

            throw new DatabaseServerDownException();
        }

        // Demonstrating retires
        if (context.GetRetryAttempt() == 1)
        {
            _logger.LogInformation("Second time submitted");

            throw new DatabaseServerDownException();
        }

        await _inventoryService.CheckAndReduceStockAsync(new CheckAndReduceStockDto
        {
            OrderId = context.Message.OrderId,
            ProductId = context.Message.ProductId,
            Quantity = context.Message.Quantity
        },
        context.CancellationToken);
    }
}
