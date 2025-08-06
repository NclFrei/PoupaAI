using AutoMapper;
using Financias.Domain.Dtos.Request;
using Financias.Domain.Models;
using Financias.Infrastructure.HttpClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financias.Application.Services;


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

        // Passando o token para o método da classe UsuarioServiceHttpClient
        var usuarioExiste = await _usuarioHttpClient.VerificarUsuarioExisteAsync(request.UsuarioId, jwtToken);

        if (!usuarioExiste)
            throw new Exception("Usuário não encontrado"); // ou lance uma exceção customizada

        // Se a validação passar, ele retorna a string "ok"
        return "ok";
    }
}

