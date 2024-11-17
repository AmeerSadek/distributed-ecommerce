﻿namespace OrdersService.Application.Contracts.Dtos;

public class CreateOrderInputDto
{
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }
    
    public int Quantity { get; set; }
}
