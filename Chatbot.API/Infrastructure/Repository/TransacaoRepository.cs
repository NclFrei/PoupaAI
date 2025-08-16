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
            .ToListAsync();

    }

    public bool ExisteTransacaoExterna(int idExternoTransacao)
    {
        return _context.Transacoes.Any(transacao => transacao.IdExterno == idExternoTransacao);
    }
}