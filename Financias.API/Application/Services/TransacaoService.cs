
using AutoMapper;
using Financias.API.Domain.DTOs.Request;
using Financias.API.Domain.DTOs.Response;
using Financias.API.Domain.Enums;
using Financias.API.Domain.Interfaces;
using Financias.API.Domain.Models;
using Financias.API.Infrastructure.RabbitMqClient;
using Financias.API.Infrastructure.Repository;
using FluentValidation;

namespace Financias.API.Application.Services;

public class TransacaoService
{
    private readonly IMapper _mapper;
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly CategoriaService _categoriaService;
    private readonly IValidator<TransacaoRequest> _validator;
    private IRabbitMqClient _rabbitMqClient;

    public TransacaoService(IMapper mapper, ITransacaoRepository transacaoRepository, IUsuarioRepository usuarioRepository, CategoriaService categoriaService, IValidator<TransacaoRequest> validator, IRabbitMqClient rabbitMqClient)
    {
        _mapper = mapper;
        _transacaoRepository = transacaoRepository;
        _usuarioRepository = usuarioRepository;
        _categoriaService = categoriaService;
        _validator = validator;
        _rabbitMqClient = rabbitMqClient;
    }

    public async Task<TransacaoResponse> CriarTransacaoAsync(TransacaoRequest transacaoRequest)
    {
        
        var result = await _validator.ValidateAsync(transacaoRequest);

        if (!result.IsValid)
        {
            var errors = result.Errors
                .Select(e => $"{e.PropertyName}: {e.ErrorMessage} ")
                .ToList();

            throw new ValidationException(string.Join(Environment.NewLine, errors));
        }
        
        var transacao = _mapper.Map<Transacao>(transacaoRequest);

        var categoria = await _categoriaService.GetCategoriaByIdAsync(transacaoRequest.CategoriaId);
        if (categoria == null)
        {
            throw new KeyNotFoundException("Categoria não encontrada.");
        }
        
        transacao.CategoriaId = transacaoRequest.CategoriaId;

        var usuario = await _usuarioRepository.BuscaUsuarioExterno(transacaoRequest.UsuarioId);
        if (usuario == null)
        {
            throw new KeyNotFoundException("Usuário não encontrado.");
        }

        transacao.UsuarioId = usuario.Id;

        if (transacao.Tipo == TipoTransacao.ENTRADA)
            usuario.Saldo += transacao.Valor;
        else if (transacao.Tipo == TipoTransacao.DESPESA)
            usuario.Saldo -= transacao.Valor;


        var transacaoCriada = await _transacaoRepository.CreateTransacaoAsync(transacao);
        await _usuarioRepository.AtualizarAsync(usuario);
        
        transacaoCriada = await _transacaoRepository.GetTransacaoComDetalhesAsync(transacao.Id);
        
        var transacaoEvent = _mapper.Map<TransacaoResponseRabbitMq>(transacaoCriada);


        _rabbitMqClient.PublicaTransacaoCriada(transacaoEvent);

        
        return _mapper.Map<TransacaoResponse>(transacaoCriada);
    }

    public async Task<TransacaoGetResponse?> GetTransacaoIdAsync(int id)
    {
        var transacao = await _transacaoRepository.GetTransacaoPorIdAsync(id);
        if (transacao == null)
            throw new KeyNotFoundException("Transação não encontrada.");
        
        return _mapper.Map<TransacaoGetResponse>(transacao);
    }
    
    public async Task DeleteTransacaoAsync(int id)
    {
        var transacao = await _transacaoRepository.GetTransacaoPorIdAsync(id);
        if (transacao == null)
            throw new KeyNotFoundException("Transação não encontrada.");

        await _transacaoRepository.DeleteTransacaoAsync(transacao); 
    }

    public async Task<Transacao> UpdateCategoriaAsync(int id, AtualizaTransacaoRequest transacaoRequest)
    {
        var transacao = await _transacaoRepository.GetTransacaoPorIdAsync(id);
        if (transacao == null)
            throw new KeyNotFoundException("Transação não encontrada.");
        
        if (transacaoRequest.CategoriaId.HasValue)
        {
            var categoriaTransacao = await _categoriaService.GetCategoriaByIdAsync(transacaoRequest.CategoriaId.Value);
            if (categoriaTransacao == null)
                throw new KeyNotFoundException("Categoria para atualizar não encontrada.");
            
            transacao.CategoriaId = transacaoRequest.CategoriaId.Value;
        }
        
        _mapper.Map(transacaoRequest, transacao);

        await _transacaoRepository.UpdateCategoriaAsync(transacao);
        return transacao;
    }
    
    public async Task<List<TransacaoGetResponse>> GetTransacaoPorUsuario(int usuarioId)
    {
        var transacoes = await _transacaoRepository.GetTransacaoPorUsuarioAsync(usuarioId);

        // garante que não seja null
        transacoes ??= new List<Transacao>();

        // mapeia para lista de DTOs
        return _mapper.Map<List<TransacaoGetResponse>>(transacoes);
    }
    
    
    
    

}

