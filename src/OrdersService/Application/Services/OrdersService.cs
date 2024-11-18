using Common.Messaging.Contracts.Events;
using MassTransit;
using OrdersService.Application.Contracts.Dtos;
using OrdersService.Application.Contracts.Interfaces;

namespace OrdersService.Application.Services;

public class OrdersService : IOrdersService
{
    private readonly IPublishEndpoint _eventPublishingEndpoint;

    public OrdersService(IPublishEndpoint eventPublishingEndpoint)
    {
        _eventPublishingEndpoint = eventPublishingEndpoint;
    }

    public async Task<CreateOrderOutputDto> CreateOrderAsync(CreateOrderInputDto createOrderDto)
    {
        await _eventPublishingEndpoint.Publish(new OrderCreatedEvent
        {
            OrderId = createOrderDto.OrderId,
            ProductId = createOrderDto.ProductId,
            Quantity = createOrderDto.Quantity
        });

        return new CreateOrderOutputDto("Your order is being processed.");
    }
}
