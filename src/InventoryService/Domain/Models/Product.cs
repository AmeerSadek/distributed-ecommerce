namespace InventoryService.Domain.Models;

internal class Product
{
    public Guid ProductId { get; set; }

    public int Stock { get; set; }
}
