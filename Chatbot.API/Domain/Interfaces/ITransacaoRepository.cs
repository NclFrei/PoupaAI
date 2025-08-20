using Chatbot.API.Domain.Models;

namespace Chatbot.API.Domain.Interfaces;

public interface ITransacaoRepository
{
    bool ExisteTransacaoExterna(int idExternoTransacao);
    Task<Transacao> CreateTransacao(Transacao transacao);
    Task<List<Transacao>> ObterTransacoesRecentesPorUsuarioAsync(int usuarioId, int limite);
    Task AtualizaTransacaoAsync(Transacao transacao);
    Task<Transacao?> GetByIdExternoAsync(int idExternoTransacao);

    Task<Transacao?> BuscaTransacaoExterno(int idExternoTransacao);
}