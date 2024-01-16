using Shared;

namespace SenderApi.Services;

public interface IMessageBusinessLogic
{
    Task<Message> CreateMessageAsync();
}
