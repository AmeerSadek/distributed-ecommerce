using InventoryService.Application.Messaging.Consumers;
using InventoryService.Exceptions;
using MassTransit;

namespace InventoryService.Application.Messaging.ConsumersDefinitions;

internal class OrderCreatedEventConsumerDefinition : ConsumerDefinition<OrderCreatedEventConsumer>
{
    public OrderCreatedEventConsumerDefinition()
    {
        EndpointName = "order-created";
    }

    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator, 
        IConsumerConfigurator<OrderCreatedEventConsumer> consumerConfigurator, 
        IRegistrationContext context)
    {
        consumerConfigurator.UseMessageRetry(x =>
        {
            x.Interval(2, 1000);
            x.Handle<DatabaseServerDownException>();
            x.Ignore<ArgumentNullException>();
        });

        endpointConfigurator.UseInMemoryOutbox(context);
    }
}
