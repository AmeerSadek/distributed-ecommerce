using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMassTransitMessaging(
        this IServiceCollection services,
        IConfiguration configuration,
        List<(Type consumer, Type ConsumerDefinition)>? consumers)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        return services.AddMassTransit(busConfigurator =>
        {
            if (consumers is not null)
            {
                foreach (var (consumer, consumerDefinition) in consumers)
                {
                    busConfigurator.AddConsumer(consumer, consumerDefinition);
                }
            }

            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["MessageBrokerSettings:Host"]!), h =>
                {
                    h.Username(configuration["MessageBrokerSettings:Username"]);
                    h.Password(configuration["MessageBrokerSettings:Password"]);
                });

                configurator.ConfigureEndpoints(context);
            });
        });
    }
}
