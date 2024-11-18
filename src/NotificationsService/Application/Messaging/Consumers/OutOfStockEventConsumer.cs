using Common.Messaging.Contracts.Events;
using MassTransit;
using System.Text;

namespace NotificationsService.Application.Messaging.Consumers;

internal class OutOfStockEventConsumer : IConsumer<OutOfStockEvent>
{
    private readonly ILogger<OutOfStockEventConsumer> _logger;

    public OutOfStockEventConsumer(ILogger<OutOfStockEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OutOfStockEvent> context)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"No enough stock for order with ID: {context.Message.OrderId}");
        stringBuilder.AppendLine($"Ordered product ID: {context.Message.ProductId}");
        stringBuilder.AppendLine($"Ordered quantity: {context.Message.Quantity}");

        _logger.LogInformation(stringBuilder.ToString());

        return Task.CompletedTask;
    }
}
