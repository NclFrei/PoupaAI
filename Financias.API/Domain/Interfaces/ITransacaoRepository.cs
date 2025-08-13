using Financias.API.Domain.Models;

namespace Financias.API.Domain.Interfaces;

public interface ITransacaoRepository
{
    Task<Transacao?> BuscarPorIdAsync(int id);
    Task<Transacao> CriarAsync(Transacao transacao);
}