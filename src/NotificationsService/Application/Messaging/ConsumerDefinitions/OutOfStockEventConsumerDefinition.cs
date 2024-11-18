using MassTransit;
using NotificationsService.Application.Messaging.Consumers;

namespace NotificationsService.Application.Messaging.ConsumerDefinitions;

internal class OutOfStockEventConsumerDefinition : ConsumerDefinition<OutOfStockEventConsumer>
{
    public OutOfStockEventConsumerDefinition()
    {
        EndpointName = "out-of-stock";
    }
}
