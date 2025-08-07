using Financias.API.Dtos.Request;
using Financias.API.Services;
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
            Console.WriteLine("---- INÍCIO DA REQUISIÇÃO ----");
            Console.WriteLine($"Método: {Request.Method}");
            Console.WriteLine($"Path: {Request.Path}");
            Console.WriteLine($"QueryString: {Request.QueryString}");

            Console.WriteLine("Headers:");
            foreach (var header in Request.Headers)
            {
                Console.WriteLine($"{header.Key}: {header.Value}");
            }

            Console.WriteLine("Corpo da requisição (JSON):");
            var requestBody = System.Text.Json.JsonSerializer.Serialize(request);
            Console.WriteLine(requestBody);
            Console.WriteLine("---- FIM DA REQUISIÇÃO ----");

            // Verifica se tem Authorization e extrai o token
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
            Console.WriteLine("---- INÍCIO DA REQUISIÇÃO ----");
            Console.WriteLine($"Método: {Request.Method}");
            Console.WriteLine($"Path: {Request.Path}");
            Console.WriteLine($"QueryString: {Request.QueryString}");

            Console.WriteLine("Headers:");
            foreach (var header in Request.Headers)
            {
                Console.WriteLine($"{header.Key}: {header.Value}");
            }

            Console.WriteLine("Corpo da requisição (JSON):");
            var requestBody = System.Text.Json.JsonSerializer.Serialize(request);
            Console.WriteLine(requestBody);
            Console.WriteLine("---- FIM DA REQUISIÇÃO ----");
            Console.WriteLine($"Erro no método RegistrarTransacao: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }


}