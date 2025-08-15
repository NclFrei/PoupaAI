namespace Chatbot.API.Domain.Interface;

public interface IJwtSettingsProvider
{
    string SecretKey { get; }
}