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
}
