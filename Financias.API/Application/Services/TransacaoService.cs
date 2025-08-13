
using AutoMapper;
using Financias.API.Domain.DTOs.Request;
using Financias.API.Domain.DTOs.Response;
using Financias.API.Domain.Enums;
using Financias.API.Domain.Interfaces;
using Financias.API.Domain.Models;
using Financias.API.Infrastructure.Repository;

namespace Financias.API.Application.Services;

public class TransacaoService
{
    private readonly IMapper _mapper;
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly CategoriaService _categoriaService;

    public TransacaoService(IMapper mapper, ITransacaoRepository transacaoRepository, IUsuarioRepository usuarioRepository, CategoriaService categoriaService)
    {
        _mapper = mapper;
        _transacaoRepository = transacaoRepository;
        _usuarioRepository = usuarioRepository;
        _categoriaService = categoriaService;
    }

    public async Task<TransacaoResponse> CriarTransacaoAsync(TransacaoRequest transacaoRequest)
    {
        
        var transacao = _mapper.Map<Transacao>(transacaoRequest);

        var categoria = await _categoriaService.GetCategoriaByIdAsync(transacaoRequest.CategoriaId);
        if (categoria == null)
        {
            throw new Exception("Categoria não encontrada.");
        }

        transacao.CategoriaId = categoria.Id;  

        var usuario = await _usuarioRepository.BuscarPorIdAsync(transacaoRequest.UsuarioId);
        if (usuario == null)
        {
            throw new Exception("Usuário não encontrado.");
        }

        if (transacao.Tipo == TipoTransacao.ENTRADA)
            usuario.Saldo += transacao.Valor;
        else if (transacao.Tipo == TipoTransacao.DESPESA)
            usuario.Saldo -= transacao.Valor;

        transacao.UsuarioId = usuario.Id;

        var transacaoCriada = await _transacaoRepository.CreateTransacaoAsync(transacao);
        await _usuarioRepository.AtualizarAsync(usuario);

        return _mapper.Map<TransacaoResponse>(transacaoCriada);
    }

    public async Task<TransacaoResponse?> GetTransacaoIdAsync(int id)
    {
        var transacao = await _transacaoRepository.GetTransacaoPorIdAsync(id);

        if (transacao == null)
        {
            return null;
        }

        return _mapper.Map<TransacaoResponse>(transacao);
    }
    
    public async Task<bool> DeleteTransacaoAsync(int id)
    {
        var transacao = await _transacaoRepository.GetTransacaoPorIdAsync(id);
        if (transacao == null)
            return false;

        return await _transacaoRepository.DeleteTransacaoAsync(transacao); 
    }

    public async Task<Transacao> UpdateCategoriaAsync(int id, AtualizaTransacaoRequest transacaoRequest)
    {
        var transacao = await _transacaoRepository.GetTransacaoPorIdAsync(id);
        if (transacao == null)
            throw new InvalidOperationException("Transação não encontrada.");
        
        if (transacaoRequest.CategoriaId.HasValue)
        {
            var categoriaTransacao = await _categoriaService.GetCategoriaByIdAsync(transacaoRequest.CategoriaId.Value);
            if (categoriaTransacao == null)
                throw new InvalidOperationException("Categoria para atualizar não encontrada.");
            
            transacao.CategoriaId = transacaoRequest.CategoriaId.Value;
        }
        
        _mapper.Map(transacaoRequest, transacao);

        await _transacaoRepository.UpdateCategoriaAsync(transacao);
        return transacao;
    }
    
    public async Task<List<Transacao?>> GetTransacaoPorUsuario(int usuarioId)
    {
        List<Transacao> transacao = await _transacaoRepository.GetTransacaoPorUsuarioAsync(usuarioId);

        if (transacao == null)
            return null;

        return transacao;
    }


}

