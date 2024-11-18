using Common;
using NotificationsService.Application.Messaging.ConsumerDefinitions;
using NotificationsService.Application.Messaging.Consumers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransitMessaging(
    builder.Configuration,
    [(typeof(InventoryUpdatedEventConsumer), typeof(InventoryUpdatedEventConsumerDefinition)),
    (typeof(OutOfStockEventConsumer), typeof(OutOfStockEventConsumerDefinition))]);

var host = builder.Build();
host.Run();
