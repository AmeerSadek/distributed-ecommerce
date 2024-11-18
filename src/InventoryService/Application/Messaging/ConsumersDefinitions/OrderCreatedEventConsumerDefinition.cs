using InventoryService.Application.Messaging.Consumers;
using MassTransit;

namespace InventoryService.Application.Messaging.ConsumersDefinitions;

internal class OrderCreatedEventConsumerDefinition : ConsumerDefinition<OrderCreatedEventConsumer>
{
    public OrderCreatedEventConsumerDefinition()
    {
        EndpointName = "order-created";
    }
}
