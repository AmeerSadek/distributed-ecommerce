using OrdersService.Application.Contracts.Dtos;

namespace OrdersService.Application.Contracts.Interfaces;

public interface IOrdersService
{
    Task<CreateOrderOutputDto> CreateOrderAsync(CreateOrderInputDto createOrderDto, CancellationToken cancellationToken);
}
