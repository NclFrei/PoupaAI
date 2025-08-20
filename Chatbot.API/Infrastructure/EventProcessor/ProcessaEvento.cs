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

    public async Task Processa(string mensagem)
    {
        using var scope = _scopeFactory.CreateScope();

        var transacaoRepository = scope.ServiceProvider.GetRequiredService<ITransacaoRepository>();

        var transacaoReadDto = JsonSerializer.Deserialize<TransacaoRequest>(mensagem);
        if (transacaoReadDto == null) return;
        
        var transacaoExiste = await transacaoRepository.BuscaTransacaoExterno(transacaoReadDto.Id);

        if (transacaoExiste == null)
        {
            var usuario = _mapper.Map<Transacao>(transacaoReadDto);
            await transacaoRepository.CreateTransacao(usuario);
        }
        else
        {
            _mapper.Map(transacaoReadDto, transacaoExiste);
            await transacaoRepository.AtualizaTransacaoAsync(transacaoExiste);
        }
    }
}