using Common.Messaging.Configuration;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Messaging.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMassTransitMessaging(
        this IServiceCollection services,
        IConfiguration configuration,
        List<(Type consumer, Type ConsumerDefinition)>? consumers)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var brokerSettings = configuration
            .GetSection(MessageBrokerSettings.SectionName)
            .Get<MessageBrokerSettings>()
            ?? throw new InvalidOperationException("Message broker settings are not configured.");

        if (brokerSettings.Host is null || brokerSettings.Username is null || brokerSettings.Password is null)
        {
            throw new InvalidOperationException("Message broker settings are not configured.");
        }

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
                configurator.Host(new Uri(brokerSettings.Host), h =>
                {
                    h.Username(brokerSettings.Username);
                    h.Password(brokerSettings.Password);
                });

                configurator.ConfigureEndpoints(context);
            });
        });
    }
}
