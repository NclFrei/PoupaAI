using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Application.Services;
using Usuarios.Domain.DTOs.Request;
using Usuarios.Domain.DTOs.Response;

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
            try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("cadastro")]
        public async Task<ActionResult<UsuarioResponse>> CriarUsuario([FromBody] UsuarioCreateRequest request)
        {
            try
            {
                var usuarioResponse = await _authService.CreateUserAsync(request);
                return Created("", usuarioResponse);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
