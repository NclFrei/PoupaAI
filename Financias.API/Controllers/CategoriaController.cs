using Financias.API.Application.Services;
using Financias.API.Domain.DTOs.Request;
using Financias.API.Domain.DTOs.Response;
using Financias.API.Domain.Models;
using Financias.API.Erros;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Financias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoriaController : ControllerBase
{
    private readonly CategoriaService _categoriaService;
 
    public CategoriaController(CategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }
 
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CategoriaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CategoriaResponse>> ObterCategoria(int id)
    {
        var categoria = await _categoriaService.GetCategoriaByIdAsync(id);
        return Ok(categoria);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(CategoriaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CategoriaResponse>> CriarCategoria([FromBody] CategoriaRequest request)
    {
        var categoriaResponse = await _categoriaService.CreateCategoriaAsync(request);
        return CreatedAtAction(nameof(ObterCategoria), new { id = categoriaResponse.Id }, categoriaResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(List<CategoriaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<CategoriaResponse>>> GetAll()
    {
        var categorias = await _categoriaService.GetAllCategoria();
        return Ok(categorias); 
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        await _categoriaService.DeleteCategoriaAsync(id); 
        return NoContent();
    }
    
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(CategoriaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiException),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update(int id, [FromBody] AtualizarCategoriaRequest request)
    {
        var categoriaAtualizada = await _categoriaService.UpdateCategoriaAsync(id, request);
        return Ok(categoriaAtualizada);
    }
    
    
    
}