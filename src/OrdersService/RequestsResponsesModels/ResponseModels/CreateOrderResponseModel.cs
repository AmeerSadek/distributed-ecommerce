namespace OrdersService.RequestsResponsesModels.ResponseModels;

public class CreateOrderResponseModel(string message)
{
    public string Message { get; set; } = message;
}
