namespace OrdersService.RequestsResponsesModels.RequestModels;

public class CreateOrderRequestModel
{
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
}
