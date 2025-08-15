using Chatbot.API.Domain.Interfaces;
using Chatbot.API.Domain.Models;
using Chatbot.API.Infrastructure.Data;

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
    
    public bool ExisteTransacaoExterna(int idExternoUsuario)
    {
        return _context.Transacoes.Any(usuario => usuario.IdExterno == idExternoUsuario);
    }
}