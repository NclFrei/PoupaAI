using Financias.API.Domain.Interfaces;
using Financias.API.Domain.Models;
using Financias.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Financias.API.Infrastructure.Repository;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly FinanciasContext _context;
 
    public TransacaoRepository(FinanciasContext context)
    {
        _context = context;
    }
 
    public async Task<Transacao?> GetTransacaoPorIdAsync(int id)
    {
        return await _context.Transacoes.FindAsync(id);
    }
 
    public async Task<Transacao> CreateTransacaoAsync(Transacao transacao)
    {
        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();
        return transacao;
    }

    public async Task<List<Transacao>> GetTransacaoPorUsuarioAsync(int idUsuario)
    {
        return await _context.Transacoes
            .Where(t => t.UsuarioId == idUsuario)
            .ToListAsync();
    }
    
    public async Task<bool> DeleteTransacaoAsync(Transacao transacao)
    {
        _context.Transacoes.Remove(transacao);
        await _context.SaveChangesAsync();
        return true;

    }

    public async Task<Transacao> UpdateCategoriaAsync(Transacao transacao)
    {
        _context.Transacoes.Update(transacao);
        await _context.SaveChangesAsync();
        return transacao;
    }
    
    public async Task<Transacao> GetTransacaoComDetalhesAsync(int id)
    {
        return await _context.Transacoes
            .Include(t => t.Categoria)
            .Include(t => t.Usuario)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
    
 
}