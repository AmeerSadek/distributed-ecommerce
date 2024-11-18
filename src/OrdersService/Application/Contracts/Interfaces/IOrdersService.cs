using OrdersService.Application.Contracts.Dtos;

namespace OrdersService.Application.Contracts.Interfaces;

public interface IOrdersService
{
    Task<CreateOrderResultDto> CreateOrderAsync(CreateOrderDto createOrderDto, CancellationToken cancellationToken);
}
