using Common.Messaging.Contracts;

namespace Infrastructure.MassTransit.Services;

internal class CommandSendingService : ICommandSendingService
{
    public Task SendAsync<T>(T message, Uri addressUri, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
