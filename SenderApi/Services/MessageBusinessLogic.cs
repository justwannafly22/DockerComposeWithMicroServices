using MassTransit;
using Shared;
using System.Reflection;

namespace SenderApi.Services;

public class MessageBusinessLogic : IMessageBusinessLogic
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<MessageBusinessLogic> _logger;

    public MessageBusinessLogic(IPublishEndpoint publishEndpoint, ILogger<MessageBusinessLogic> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task<Message> CreateMessageAsync()
    {
        var message = new Message()
        {
            Content = "Newly created message",
            Sender = Assembly.GetExecutingAssembly().ToString(),
            CreatedAt = DateTimeOffset.Now
        };
        _logger.LogInformation($"Message: {message.Id} successfully created.");

        await _publishEndpoint.Publish(message);
        _logger.LogInformation($"Message: {message.Id} successfully pushed.");

        return message;
    }
}
