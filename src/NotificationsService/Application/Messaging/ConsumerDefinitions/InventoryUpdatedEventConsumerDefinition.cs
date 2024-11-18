using MassTransit;
using NotificationsService.Application.Messaging.Consumers;

namespace NotificationsService.Application.Messaging.ConsumerDefinitions;

internal class InventoryUpdatedEventConsumerDefinition 
    : ConsumerDefinition<InventoryUpdatedEventConsumer>
{
    public InventoryUpdatedEventConsumerDefinition()
    {
        EndpointName = "inventory-updated";
    }

    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator, 
        IConsumerConfigurator<InventoryUpdatedEventConsumer> consumerConfigurator, 
        IRegistrationContext context)
    {
        consumerConfigurator.UseMessageRetry(x =>
        {
            x.Interval(3, 1000);
        });
    }
}
