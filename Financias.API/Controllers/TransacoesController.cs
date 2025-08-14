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
    public async Task<ActionResult<TransacaoResponse>> GetById(int id)
    {
        var transacao = await _transacaoService.GetTransacaoIdAsync(id);
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
        await _transacaoService.DeleteTransacaoAsync(id); 
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
    
    [HttpGet("usuario/{idUsuario}")]
    public async Task<ActionResult<List<Transacao>>> GetTransacaoUsuario(int idUsuario)
    {
        var transacoes = await _transacaoService.GetTransacaoPorUsuario(idUsuario);
        return Ok(transacoes); 
    }
    
  
    
}