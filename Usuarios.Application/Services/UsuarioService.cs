using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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

[Authorize]
public class UsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    public async Task<UsuarioResponse> CreateUserAsync(UsuarioCreateRequest userRequest)
    {
        if (await _usuarioRepository.VerificaEmailExisteAsync(userRequest.Email))
            throw new InvalidOperationException("Email já cadastrado.");

        var usuario = _mapper.Map<Usuario>(userRequest);
        var usuarioCriado = await _usuarioRepository.CriarAsync(usuario);

        return _mapper.Map<UsuarioResponse>(usuarioCriado);
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
        
      
}


