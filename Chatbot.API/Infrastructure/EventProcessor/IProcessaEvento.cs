namespace Chatbot.API.Infrastructure.EventProcessor;

public interface IProcessaEvento
{
    Task Processa(string mensagem);
}