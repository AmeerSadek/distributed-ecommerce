using Common;
using InventoryService.Application.Contracts;
using InventoryService.Application.Messaging.Consumers;
using InventoryService.Application.Messaging.ConsumersDefinitions;
using InventoryService.DataAccess.Repository;
using InventoryService.Domain.Repositories;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransitMessaging(
    builder.Configuration,
    [(typeof(OrderCreatedEventConsumer), typeof(OrderCreatedEventConsumerDefinition))]);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInventoryService, InventoryService.Application.Services.InventoryService>();

var host = builder.Build();
host.Run();
