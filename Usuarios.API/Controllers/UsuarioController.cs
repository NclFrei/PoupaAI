using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Application.Services;
using Usuarios.Domain.DTOs.Request;
using Usuarios.Domain.DTOs.Response;

namespace Usuarios.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuarioController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioResponse>> ObterUsuario(int id)
    {
        var usuario = await _usuarioService.GetUserByIdAsync(id);
        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarUsuario(int id)
    {
        var delete = await _usuarioService.DeleteAsync(id);
        if (!delete)
            return NotFound($"Não foi possível remover: usuário com ID {id} não encontrado.");

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> AtualizarPerfil(int id, [FromBody] AtualizarUsuarioRequest request)
    {
        var atualizado = await _usuarioService.AtualizarPerfilAsync(id, request);
        return Ok(atualizado);
    }
}