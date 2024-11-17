namespace OrdersService.ViewModels.OutputViewModels;

public class CreateOrderOutputViewModel(string message)
{
    public string Message { get; set; } = message;
}
