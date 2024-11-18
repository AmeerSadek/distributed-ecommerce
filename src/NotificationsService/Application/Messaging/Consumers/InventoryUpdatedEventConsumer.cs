using Common.Messaging.Contracts.Events;
using MassTransit;
using System.Text;

namespace NotificationsService.Application.Messaging.Consumers;

internal class InventoryUpdatedEventConsumer : IConsumer<InventoryUpdatedEvent>
{
    private readonly ILogger<InventoryUpdatedEventConsumer> _logger;

    public InventoryUpdatedEventConsumer(ILogger<InventoryUpdatedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<InventoryUpdatedEvent> context)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"The inventory is updated for order with ID: {context.Message.OrderId}");
        stringBuilder.AppendLine($"Ordered product ID: {context.Message.ProductId}");
        stringBuilder.AppendLine($"Ordered quantity: {context.Message.Quantity}");

        _logger.LogInformation(stringBuilder.ToString());

        return Task.CompletedTask;
    }
}
