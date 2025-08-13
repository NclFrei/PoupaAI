using Financias.API.Domain.Models;

namespace Financias.API.Domain.Interfaces;

public interface ITransacaoRepository
{
    Task<Transacao?> GetTransacaoPorIdAsync(int id);
    Task<Transacao> CreateTransacaoAsync(Transacao transacao);
    Task<Transacao> UpdateCategoriaAsync(Transacao transacao);
    Task<bool> DeleteTransacaoAsync(Transacao transacao);

    Task<List<Transacao>> GetTransacaoPorUsuarioAsync(int idUsuario);
}