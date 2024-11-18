using InventoryService.Application.Contracts;
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

    public async Task CheckAndReduceStockAsync(Guid productId, int quantity)
    {
        var isReductionSuccess = await _productsRepository.ReduceStockAsync(productId, quantity);

        if (isReductionSuccess)
        {
            await Console.Out.WriteLineAsync($"Success: Prodcut ordered: {productId} | Ordered quantity = {quantity}");
            // publish InventoryUpdated event
        }
        else
        {
            await Console.Out.WriteLineAsync($"Failed: Prodcut ordered: {productId} | Ordered quantity = {quantity}");
            // publish OutOfStock event
        }
    }
}
