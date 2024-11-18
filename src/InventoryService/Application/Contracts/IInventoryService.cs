namespace InventoryService.Application.Contracts;

internal interface IInventoryService
{
    Task CheckAndReduceStockAsync(Guid productId, int quantity);
}
