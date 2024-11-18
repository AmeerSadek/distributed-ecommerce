using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using OrdersService.Application.Contracts.Dtos;
using OrdersService.Application.Contracts.Interfaces;
using OrdersService.RequestsResponsesModels.RequestModels;
using OrdersService.RequestsResponsesModels.ResponseModels;

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
    [ProducesResponseType(typeof(CreateOrderResponseModel), StatusCodes.Status202Accepted)]
    [Consumes(typeof(CreateOrderRequestModel), "application/json")]
    [Produces(typeof(CreateOrderResponseModel))]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequestModel createOrderInputViewModel, CancellationToken cancellationToken)
    {
        var createOrderResultDto = await _ordersService.CreateOrderAsync(
            new CreateOrderDto(
                createOrderInputViewModel.OrderId, 
                createOrderInputViewModel.ProductId, 
                createOrderInputViewModel.Quantity),
            cancellationToken); ;

        return Accepted(new CreateOrderResponseModel(createOrderResultDto.Message));
    }
}
