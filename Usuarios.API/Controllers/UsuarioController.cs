using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Usuarios.API.Application.Services;
using Usuarios.API.Domain.DTOs.Request;
using Usuarios.API.Domain.DTOs.Response;
using Usuarios.API.Erros;

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
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UsuarioResponse>> ObterUsuario(int id)
    {
        var usuario = await _usuarioService.GetUserByIdAsync(id);
        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeletarUsuario(int id)
    {
        var delete = await _usuarioService.DeleteAsync(id);
        if (!delete)
            return NotFound($"Não foi possível remover: usuário com ID {id} não encontrado.");

        return NoContent();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarPerfil(int id, [FromBody] AtualizarUsuarioRequest request)
    {
        var atualizado = await _usuarioService.AtualizarPerfilAsync(id, request);
        return Ok(atualizado);
    }
}