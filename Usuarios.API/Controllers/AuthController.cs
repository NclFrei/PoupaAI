using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Application.Services;
using Usuarios.Domain.DTOs.Request;
using Usuarios.Domain.DTOs.Response;
using Usuarios.Domain.ExceptionsBase;
using FluentValidation;

namespace Usuarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }

        [HttpPost("cadastro")]
        public async Task<ActionResult<UsuarioResponse>> CriarUsuario([FromBody] UsuarioCreateRequest request)
        {
            var usuarioResponse = await _authService.CreateUserAsync(request);
            return CreatedAtAction(null, usuarioResponse); // Você pode substituir null pela action de obter usuário
        }
    }
}
