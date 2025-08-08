using Financias.API.Application.Services;
using Financias.API.Dtos.Request;
using Microsoft.AspNetCore.Mvc;

namespace Financias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly TransacaoService _transacaoService;

    public TransacoesController(TransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarTransacao([FromBody] TransacaoRequest request)
    {
        try
        {
          
            var requestBody = System.Text.Json.JsonSerializer.Serialize(request);

            if (!Request.Headers.ContainsKey("Authorization") ||
                !Request.Headers["Authorization"].ToString().StartsWith("Bearer "))
            {
                return Unauthorized("Token de autorização ausente ou inválido.");
            }

            var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Trim();

            Console.WriteLine($"Token extraído do header: {jwtToken}");

            var resultado = await _transacaoService.RegistrarTransacaoAsync(request, jwtToken);

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            var requestBody = System.Text.Json.JsonSerializer.Serialize(request);
            Console.WriteLine(requestBody);
            Console.WriteLine("---- FIM DA REQUISIÇÃO ----");
            Console.WriteLine($"Erro no método RegistrarTransacao: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }


}