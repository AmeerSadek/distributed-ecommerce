namespace Common.Messaging.Configuration;

public class MessageBrokerSettings
{
    public const string SectionName = "MessageBrokerSettings";

    public string Host { get; set; } = default!;

    public string Username { get; set; } = default!;

    public string Password { get; set; } = default!;
}
