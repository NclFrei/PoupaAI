using AutoMapper;
using Usuarios.API.Domain.DTOs.Request;
using Usuarios.API.Domain.DTOs.Response;
using Usuarios.API.Domain.Interfaces;
using Usuarios.API.Domain.Models;
using Usuarios.API.Infrastructure.RabbitMqClient;


namespace Usuarios.API.Application.Services;

public class UsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;
    private IRabbitMqClient _rabbitMqClient;

    public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, IRabbitMqClient rabbitMqClient)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
        _rabbitMqClient = rabbitMqClient;
    }

    public async Task<UsuarioResponse?> GetUserByIdAsync(int id)
    {
        var usuario = await _usuarioRepository.BuscarPorIdAsync(id);

        if (usuario == null)
            return null;

        return _mapper.Map<UsuarioResponse>(usuario);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var usuario = await _usuarioRepository.BuscarPorIdAsync(id);
        if (usuario == null)
            return false;

        return await _usuarioRepository.DeleteAsync(usuario); 
    }

    public async Task<UsuarioResponse> AtualizarPerfilAsync(int id, AtualizarUsuarioRequest request)
    {
        var usuario = await _usuarioRepository.BuscarPorIdAsync(id);
        if (usuario == null)
            throw new InvalidOperationException("Usuário não encontrado.");

        _mapper.Map(request, usuario);

        if (!string.IsNullOrWhiteSpace(request.Senha))
            usuario.SetPassword(request.Senha);

        await _usuarioRepository.AtualizarAsync(usuario);
        
        var usuarioResponseEvent = _mapper.Map<UsuarioResponseEvent>(usuario);
        _rabbitMqClient.PublicaUsuario(usuarioResponseEvent);


        return _mapper.Map<UsuarioResponse>(usuario);
    }


}


