using OrdersService.Application.Contracts.Dtos;
using OrdersService.Application.Contracts.Interfaces;

namespace OrdersService.Application.Services;

public class OrdersService : IOrdersService
{
    public Task<CreateOrderOutputDto> CreateOrderAsync(CreateOrderInputDto createOrderDto)
    {
        throw new NotImplementedException();
    }
}
