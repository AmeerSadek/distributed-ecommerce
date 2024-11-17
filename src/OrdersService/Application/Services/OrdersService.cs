using OrdersService.Application.Contracts.Dtos;
using OrdersService.Application.Contracts.Interfaces;

namespace OrdersService.Application.Services;

public class OrdersService : IOrdersService
{
    public async Task<CreateOrderOutputDto> CreateOrderAsync(CreateOrderInputDto createOrderDto)
    {
        await Task.Delay(10);

        return new CreateOrderOutputDto("");
    }
}
