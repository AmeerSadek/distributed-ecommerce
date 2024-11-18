namespace OrdersService.Application.Contracts.Dtos;

public class CreateOrderDto(Guid orderId, Guid productId, int quantity)
{
    public Guid OrderId { get; set; } = orderId;

    public Guid ProductId { get; set; } = productId;
    
    public int Quantity { get; set; } = quantity;
}
