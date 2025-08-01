using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.DTOs.Request;
using Usuarios.Domain.DTOs.Response;
using Usuarios.Domain.Interfaces;
using Usuarios.Domain.Models;
using Usuarios.Infrastructure.Data;

namespace Usuarios.Application.Services;

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


