using AutoMapper;
using Usuarios.API.Domain.DTOs.Request;
using Usuarios.API.Domain.DTOs.Response;
using Usuarios.API.Domain.Interfaces;
using Usuarios.API.Domain.Models;


namespace Usuarios.API.Application.Services;

public class UsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
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

    public async Task<Usuario> AtualizarPerfilAsync(int id, AtualizarUsuarioRequest request)
    {
        var usuario = await _usuarioRepository.BuscarPorIdAsync(id);
        if (usuario == null)
            throw new InvalidOperationException("Usuário não encontrado.");

        _mapper.Map(request, usuario);

        if (!string.IsNullOrWhiteSpace(request.Senha))
            usuario.SetPassword(request.Senha);

        await _usuarioRepository.AtualizarAsync(usuario);
        return usuario;
    }


}


