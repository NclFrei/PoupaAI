using System.Text.Json;
using AutoMapper;
using Chatbot.API.Domain.DTOs.Request;
using Chatbot.API.Domain.Interfaces;
using Chatbot.API.Domain.Models;

namespace Chatbot.API.Infrastructure.EventProcessor;

public class ProcessaEvento : IProcessaEvento
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;

    public ProcessaEvento(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
        _scopeFactory = scopeFactory;
        _mapper = mapper;
    }

    public void Processa(string mensagem)
    {
        using var scope = _scopeFactory.CreateScope();

        var transacaoRepository = scope.ServiceProvider.GetRequiredService<ITransacaoRepository>();

        var transacaoReadDto = JsonSerializer.Deserialize<TransacaoRequest>(mensagem);

        var transacao = _mapper.Map<Transacao>(transacaoReadDto);

        if (!transacaoRepository.ExisteTransacaoExterna(transacao.IdExterno))
        {
            transacaoRepository.CreateTransacao(transacao);
        }
    }
}