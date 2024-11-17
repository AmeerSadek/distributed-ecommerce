using Common.Messaging.Contracts;

namespace Infrastructure.MassTransit.Services;

internal class EventPublishingService : IEventPublishingService
{
    public Task PublishAsync<T>(T publishedEvent, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
