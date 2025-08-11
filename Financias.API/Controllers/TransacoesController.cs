using Financias.API.Application.Services;
using Financias.API.Domain.DTOs.Request;
using Financias.API.Domain.DTOs.Response;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var transacao = await _transacaoService.GetTransacaoIdAsync(id);
        if (transacao == null)
        {
            return NotFound();
        }

        return Ok(transacao);


    }

    [HttpPost]
    public async Task<ActionResult<TransacaoResponse>> Create([FromBody] TransacaoRequest transacaoRequest)
    {
        var response = await _transacaoService.CriarTransacaoAsync(transacaoRequest);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
}