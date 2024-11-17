using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using OrdersService.Application.Contracts.Dtos;
using OrdersService.Application.Contracts.Interfaces;
using OrdersService.ViewModels.InputViewModels;
using OrdersService.ViewModels.OutputViewModels;

namespace OrdersService.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/orders")]
public class OrdersController : Controller
{
    private readonly IOrdersService _ordersService;

    public OrdersController(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateOrderOutputViewModel), StatusCodes.Status202Accepted)]
    public async Task<IActionResult> CreateOrderAsync(CreateOrderInputViewModel createOrderInputViewModel, CancellationToken cancellationToken)
    {
        var createOrderOutputDto = await _ordersService.CreateOrderAsync(new CreateOrderInputDto
        {
            ProductId = createOrderInputViewModel.ProductId,
            Quantity = createOrderInputViewModel.Quantity
        });

        return Accepted(createOrderOutputDto);
    }
}
