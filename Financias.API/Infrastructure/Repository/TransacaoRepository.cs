using Financias.API.Domain.Interfaces;
using Financias.API.Domain.Models;
using Financias.API.Infrastructure.Data;

namespace Financias.API.Infrastructure.Repository;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly FinanciasContext _context;
 
    public TransacaoRepository(FinanciasContext context)
    {
        _context = context;
    }
 
    public async Task<Transacao?> BuscarPorIdAsync(int id)
    {
        return await _context.Transacoes.FindAsync(id);
    }
 
    public async Task<Transacao> CriarAsync(Transacao transacao)
    {
        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();
        return transacao;
    }
 
}