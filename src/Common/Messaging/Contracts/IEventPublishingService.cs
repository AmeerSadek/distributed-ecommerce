namespace Common.Messaging.Contracts;

public interface IEventPublishingService
{
    Task PublishAsync<T>(T publishedEvent, CancellationToken cancellationToken = default);
}
