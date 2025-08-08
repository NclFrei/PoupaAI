
using AutoMapper;
using Financias.API.Domain.DTOs.Request;
using Financias.API.HttpClients;

namespace Financias.API.Application.Services;

public class TransacaoService
{
    private readonly UsuarioServiceHttpClient _usuarioHttpClient;
    private readonly IMapper _mapper;

    public TransacaoService(UsuarioServiceHttpClient usuarioHttpClient, IMapper mapper)
    {
        _usuarioHttpClient = usuarioHttpClient;
        _mapper = mapper;
    }

    public async Task<string> RegistrarTransacaoAsync(TransacaoRequest request, string jwtToken)
    {
        Console.WriteLine($"JWT Token recebido: {jwtToken}");

        var usuarioExiste = await _usuarioHttpClient.VerificarUsuarioExisteAsync(request.UsuarioId, jwtToken);

        if (!usuarioExiste)
            throw new Exception("Usuário não encontrado"); 

      
        return "ok";
    }
}

