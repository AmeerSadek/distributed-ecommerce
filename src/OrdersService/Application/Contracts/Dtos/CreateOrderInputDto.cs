namespace OrdersService.Application.Contracts.Dtos;

public class CreateOrderInputDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
