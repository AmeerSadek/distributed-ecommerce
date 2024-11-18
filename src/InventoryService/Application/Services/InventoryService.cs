using Common.Messaging.Contracts.Events;
using InventoryService.Application.Contracts.Dtos;
using InventoryService.Application.Contracts.Interfaces;
using InventoryService.Domain.Repositories;
using MassTransit;

namespace InventoryService.Application.Services;

internal class InventoryService : IInventoryService
{
    private readonly IProductRepository _productsRepository;
    private readonly IPublishEndpoint _eventPublishingEndpoint;

    public InventoryService(
        IProductRepository productsRepository,
        IPublishEndpoint eventPublishingEndpoint)
    {
        _productsRepository = productsRepository;
        _eventPublishingEndpoint = eventPublishingEndpoint;
    }

    public async Task CheckAndReduceStockAsync(
        CheckAndReduceStockDto checkAndReduceStockDto,
        CancellationToken cancellationToken)
    {
        var isReductionSuccess = await _productsRepository.ReduceStockAsync(
            checkAndReduceStockDto.ProductId,
            checkAndReduceStockDto.Quantity);

        if (isReductionSuccess)
        {
            await _eventPublishingEndpoint.Publish(
                new InventoryUpdatedEvent(checkAndReduceStockDto.OrderId, checkAndReduceStockDto.ProductId, checkAndReduceStockDto.Quantity),
                cancellationToken);
        }
        else
        {
            await _eventPublishingEndpoint.Publish(
                new OutOfStockEvent(checkAndReduceStockDto.OrderId, checkAndReduceStockDto.ProductId, checkAndReduceStockDto.Quantity),
                cancellationToken);
        }
    }
}
