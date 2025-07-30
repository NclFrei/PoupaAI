using AutoMapper;
using BCrypt.Net;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.Mapper;
using Usuarios.Domain.DTOs.Request;
using Usuarios.Domain.DTOs.Response;
using Usuarios.Domain.ExceptionsBase;
using Usuarios.Domain.Interfaces;
using Usuarios.Domain.Models;

namespace Usuarios.Application.Services;

public class AuthService
{

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly TokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly IValidator<UsuarioCreateRequest> _validator;

    public AuthService(IUsuarioRepository usuarioRepository, TokenService tokenService, IMapper mapper, IValidator<UsuarioCreateRequest> validator)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {

        var usuario = await _usuarioRepository.BuscarPorEmailAsync(request.Email);

        if (usuario == null)
            throw new Exception("Usuário não encontrado.");


        if (!usuario.CheckPassword(request.Senha))
            throw new Exception("Senha inválida.");


        var token = _tokenService.Generate(usuario);


        return new LoginResponse
        {
            Token = token,
            Email = usuario.Email,
            Nome = usuario.Nome
        };
    }

    public async Task<UsuarioResponse> CreateUserAsync(UsuarioCreateRequest userRequest)
    {
        var result = await _validator.ValidateAsync(userRequest);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }

        if (await _usuarioRepository.VerificaEmailExisteAsync(userRequest.Email))
            throw new InvalidOperationException("Email já cadastrado.");

        var usuario = _mapper.Map<Usuario>(userRequest);
        usuario.SetPassword(userRequest.Senha);
        var usuarioCriado = await _usuarioRepository.CriarAsync(usuario);

        return _mapper.Map<UsuarioResponse>(usuarioCriado);
    }
}
