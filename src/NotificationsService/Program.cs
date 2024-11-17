using Common;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransitMessaging(builder.Configuration, null);

var host = builder.Build();
host.Run();
