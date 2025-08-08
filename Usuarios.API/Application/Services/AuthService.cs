using AutoMapper;
using FluentValidation;
using Usuarios.API.Domain.DTOs.Request;
using Usuarios.API.Domain.DTOs.Response;
using Usuarios.API.Domain.Interfaces;
using Usuarios.API.Domain.Models;
using Usuarios.API.Infrastructure.RabbitMqClient;


namespace Usuarios.API.Application.Services;

public class AuthService
{

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly TokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly IValidator<UsuarioCreateRequest> _validator;
    private IRabbitMqClient _rabbitMqClient;



    public AuthService(IUsuarioRepository usuarioRepository, TokenService tokenService, IMapper mapper, IValidator<UsuarioCreateRequest> validator, IRabbitMqClient rabbitMqClient)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
        _mapper = mapper;
        _validator = validator;
        _rabbitMqClient = rabbitMqClient;
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
           var errors = result.Errors
                .Select(e => $"{e.PropertyName}: {e.ErrorMessage} ")
                    .ToList();

                throw new ValidationException(string.Join(Environment.NewLine, errors));
        }

        if (await _usuarioRepository.VerificaEmailExisteAsync(userRequest.Email))
            throw new InvalidOperationException("Email já cadastrado.");

        var usuario = _mapper.Map<Usuario>(userRequest);
        usuario.SetPassword(userRequest.Senha);
        var usuarioCriado = await _usuarioRepository.CriarAsync(usuario);

        _rabbitMqClient.PublicaUsuarioCriado(usuarioCriado);

        return _mapper.Map<UsuarioResponse>(usuarioCriado);
    }
}
