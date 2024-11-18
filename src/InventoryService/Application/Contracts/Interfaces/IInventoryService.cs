using InventoryService.Application.Contracts.Dtos;

namespace InventoryService.Application.Contracts.Interfaces;

internal interface IInventoryService
{
    Task CheckAndReduceStockAsync(CheckAndReduceStockDto checkAndReduceStockDto, CancellationToken cancellationToken);
}
