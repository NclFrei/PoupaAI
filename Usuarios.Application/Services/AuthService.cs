using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.DTOs.Request;
using Usuarios.Domain.DTOs.Response;
using Usuarios.Domain.Interfaces;

namespace Usuarios.Application.Services;

public class AuthService
{

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly TokenService _tokenService;

    public AuthService(IUsuarioRepository usuarioRepository, TokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {

        var usuario = await _usuarioRepository.BuscarPorEmailAsync(request.Email);

        if (usuario == null)
            throw new Exception("Usuário não encontrado.");


        if (usuario.Senha != request.Senha)
            throw new Exception("Senha inválida.");


        var token = _tokenService.Generate(usuario);


        return new LoginResponse
        {
            Token = token,
            Email = usuario.Email,
            Nome = usuario.Nome 
        };
    }
}
