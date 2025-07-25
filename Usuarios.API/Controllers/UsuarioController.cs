using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Application.Services;
using Usuarios.Domain.DTOs.Request;
using Usuarios.Domain.DTOs.Response;

namespace Usuarios.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{

    private readonly UsuarioService _usuarioService;

    public UsuarioController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioResponse>> CriarUsuario([FromBody] UsuarioCreateRequest request)
    {
        try
        {
            var usuarioResponse = await _usuarioService.CreateUserAsync(request);
            return CreatedAtAction(nameof(ObterUsuario), new { id = usuarioResponse.Id }, usuarioResponse);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioResponse>> ObterUsuario(int id)
    {
        var usuario = await _usuarioService.GetUserByIdAsync(id);

        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }

}
