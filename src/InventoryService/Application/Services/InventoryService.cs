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
    private readonly ILogger<InventoryService> _logger;

    public InventoryService(
        IProductRepository productsRepository,
        IPublishEndpoint eventPublishingEndpoint,
        ILogger<InventoryService> logger)
    {
        _productsRepository = productsRepository;
        _eventPublishingEndpoint = eventPublishingEndpoint;
        _logger = logger;
    }

    public async Task CheckAndReduceStockAsync(
        CheckAndReduceStockDto checkAndReduceStockDto,
        CancellationToken cancellationToken)
    {
        bool isStockReducedSuccessfully;

        try
        {
            isStockReducedSuccessfully = await _productsRepository.ReduceStockAsync(
                checkAndReduceStockDto.ProductId,
                checkAndReduceStockDto.Quantity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while reducing product stock");
            isStockReducedSuccessfully = false;
        }

        if (isStockReducedSuccessfully)
        {
            try
            {
                await _eventPublishingEndpoint.Publish(
                    new InventoryUpdatedEvent(checkAndReduceStockDto.OrderId, checkAndReduceStockDto.ProductId, checkAndReduceStockDto.Quantity),
                    cancellationToken);

                _logger.LogInformation("InventoryUpdated event published successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while publishing InventoryUpdated event");
            }
        }
        else
        {
            try
            {
                await _eventPublishingEndpoint.Publish(
                    new OutOfStockEvent(checkAndReduceStockDto.OrderId, checkAndReduceStockDto.ProductId, checkAndReduceStockDto.Quantity),
                    cancellationToken);

                _logger.LogInformation("OutOfStock event published successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while publishing OutOfStock event");
            }
        }
    }
}
