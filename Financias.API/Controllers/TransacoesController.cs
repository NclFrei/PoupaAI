using Financias.API.Application.Services;
using Financias.API.Domain.DTOs.Request;
using Financias.API.Domain.DTOs.Response;
using Financias.API.Domain.Models;
using Financias.API.Erros;
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
    [ProducesResponseType(typeof(TransacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TransacaoResponse>> GetById(int id)
    {
        var transacao = await _transacaoService.GetTransacaoIdAsync(id);
        return Ok(transacao);
    }
    
    

    [HttpPost]
    [ProducesResponseType(typeof(TransacaoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TransacaoResponse>> Create([FromBody] TransacaoRequest transacaoRequest)
    {
        var response = await _transacaoService.CriarTransacaoAsync(transacaoRequest);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        await _transacaoService.DeleteTransacaoAsync(id); 
        return NoContent(); 
    }
    
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(TransacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update(int id, [FromBody] AtualizaTransacaoRequest request)
    {
        try
        {
            var transacaoAtualizada = await _transacaoService.UpdateTransacaoAsync(id, request);
            return Ok(transacaoAtualizada);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpGet("usuario/{idUsuario}")]
    [ProducesResponseType(typeof(List<Transacao>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<Transacao>>> GetTransacaoUsuario(int idUsuario)
    {
        var transacoes = await _transacaoService.GetTransacaoPorUsuario(idUsuario);
        return Ok(transacoes); 
    }
    
  
    
}