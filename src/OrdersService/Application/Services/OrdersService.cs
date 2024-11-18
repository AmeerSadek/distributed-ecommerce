using Common.Messaging.Contracts.Events;
using MassTransit;
using OrdersService.Application.Contracts.Dtos;
using OrdersService.Application.Contracts.Interfaces;

namespace OrdersService.Application.Services;

public class OrdersService : IOrdersService
{
    private readonly IPublishEndpoint _eventPublishingEndpoint;
    private readonly ILogger<OrdersService> _logger;

    public OrdersService(
        IPublishEndpoint eventPublishingEndpoint, 
        ILogger<OrdersService> logger)
    {
        _eventPublishingEndpoint = eventPublishingEndpoint;
        _logger = logger;
    }

    public async Task<CreateOrderResultDto> CreateOrderAsync(
        CreateOrderDto createOrderDto,
        CancellationToken cancellationToken)
    {
        try
        {
            await _eventPublishingEndpoint.Publish(
                new OrderCreatedEvent(createOrderDto.OrderId, createOrderDto.ProductId, createOrderDto.Quantity),
                cancellationToken);

            _logger.LogInformation("OrderCreated event published successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while publishing OrderCreated event");
        }

        return new CreateOrderResultDto("Your order is being processed.");
    }
}
