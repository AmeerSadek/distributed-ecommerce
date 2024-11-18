namespace InventoryService.Application.Contracts.Dtos;

internal class CheckAndReduceStockDto
{
    public Guid OrderId { get; init; }

    public Guid ProductId { get; init; }

    public int Quantity { get; init; }
}
