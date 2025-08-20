using Chatbot.API.Domain.Interfaces;
using Chatbot.API.Domain.Models;
using Chatbot.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.API.Infrastructure.Repository;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly ChatbotContext _context;

    public TransacaoRepository(ChatbotContext context)
    {
        _context = context;
    }

    public async Task<Transacao> CreateTransacao(Transacao transacao)
    {
        _context.Transacoes.Add(transacao);
        _context.SaveChanges();
        return transacao;
    }

    public async Task<List<Transacao>> ObterTransacoesRecentesPorUsuarioAsync(int usuarioId, int limite)
    {
        return await _context.Transacoes
            .Where(t => t.UsuarioId == usuarioId)
            .OrderBy(t => t.DataTransacao)
            .Take(limite) 
            .ToListAsync();
    }

    public bool ExisteTransacaoExterna(int idExternoTransacao)
    {
        return _context.Transacoes.Any(transacao => transacao.IdExterno == idExternoTransacao);
    }
    
    public async Task<Transacao?> GetByIdExternoAsync(int idExternoTransacao)
    {
        return await _context.Transacoes
            .FirstOrDefaultAsync(t => t.IdExterno == idExternoTransacao);
    }
    
    public async Task AtualizaTransacaoAsync(Transacao transacao)
    {
        _context.Transacoes.Update(transacao);
        await _context.SaveChangesAsync();
    }
    
    public async Task<Transacao?> BuscaTransacaoExterno(int idExternoTransacao)
    {
        return await _context.Transacoes.FindAsync(idExternoTransacao);
    }
    
}