namespace Common.Messaging.Contracts;

public interface ICommandSendingService
{
    Task SendAsync<T>(T message, Uri addressUri, CancellationToken cancellationToken = default);
}
