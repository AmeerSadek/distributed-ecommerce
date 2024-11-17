namespace OrdersService.Application.Contracts.Dtos;

public class CreateOrderOutputDto(string message)
{
    public string Message { get; set; } = message;
}
