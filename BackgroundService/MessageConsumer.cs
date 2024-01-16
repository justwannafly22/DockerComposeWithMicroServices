using MassTransit;
using Shared;

namespace BackgroundService;

public class MessageConsumer : IConsumer<Message>
{
    private readonly ILogger<MessageConsumer> _logger;

    public MessageConsumer(ILogger<MessageConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<Message> context)
    {
        try
        {
            _logger.LogInformation($"Message: {context.Message.Id} received with content: {context.Message.Content} from sender: {context.Message.Sender}.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing message.");
        }

        return Task.CompletedTask;
    }
}
