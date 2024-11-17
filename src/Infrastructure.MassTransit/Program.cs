using Common.Messaging.Contracts;
using Infrastructure.MassTransit;
using Infrastructure.MassTransit.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();

builder.Services.AddScoped<IEventPublishingService, EventPublishingService>();
builder.Services.AddScoped<ICommandSendingService, CommandSendingService>();

var host = builder.Build();
host.Run();
