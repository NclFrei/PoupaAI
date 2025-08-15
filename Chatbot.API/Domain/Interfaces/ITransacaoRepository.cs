using Chatbot.API.Domain.Models;

namespace Chatbot.API.Domain.Interfaces;

public interface ITransacaoRepository
{
    bool ExisteTransacaoExterna(int idExternoTransacao);
    Task<Transacao> CreateTransacao(Transacao transacao);
}