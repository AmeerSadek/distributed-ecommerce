using MassTransit;
using NotificationsService.Application.Messaging.Consumers;

namespace NotificationsService.Application.Messaging.ConsumerDefinitions;

internal class OutOfStockEventConsumerDefinition : ConsumerDefinition<OutOfStockEventConsumer>
{
    public OutOfStockEventConsumerDefinition()
    {
        EndpointName = "out-of-stock";
    }

    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator, 
        IConsumerConfigurator<OutOfStockEventConsumer> consumerConfigurator, 
        IRegistrationContext context)
    {
        consumerConfigurator.UseMessageRetry(x =>
        {
            x.Interval(3, 1000);
        });
    }
}
