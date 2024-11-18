namespace OrdersService.Application.Contracts.Dtos;

public class CreateOrderResultDto(string message)
{
    public string Message { get; set; } = message;
}
