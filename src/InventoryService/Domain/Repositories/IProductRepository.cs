namespace InventoryService.Domain.Repositories;

internal interface IProductRepository
{
    ValueTask<bool> ReduceStockAsync(Guid productId, int quantity);
}
