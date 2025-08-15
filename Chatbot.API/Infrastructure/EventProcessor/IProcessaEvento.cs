namespace Chatbot.API.Infrastructure.EventProcessor;

public interface IProcessaEvento
{
    void Processa(string mensagem);
}