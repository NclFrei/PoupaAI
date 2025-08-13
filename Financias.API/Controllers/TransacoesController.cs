using Financias.API.Application.Services;
using Financias.API.Domain.DTOs.Request;
using Financias.API.Domain.DTOs.Response;
using Financias.API.Domain.Models;
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
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        bool deleted = await _transacaoService.DeleteTransacaoAsync(id);
        if (!deleted)
            return NotFound("Transação não encontrada.");

        return NoContent();
    }
    
    [HttpPatch("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] AtualizaTransacaoRequest request)
    {
        try
        {
            var transacaoAtualizada = await _transacaoService.UpdateCategoriaAsync(id, request);
            return Ok(transacaoAtualizada);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpGet("{idUsuario}")]
    public async Task<ActionResult<List<Transacao>>> GetTransacaoUsuario(int idUsuario)
    {
        var transacao = await _transacaoService.GetTransacaoPorUsuario(idUsuario);
        if (transacao == null || transacao.Count == 0)
            return NotFound("Nenhuma transacao encontrada.");

        return Ok(transacao);
    }
}