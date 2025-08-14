using Financias.API.Application.Services;
using Financias.API.Domain.DTOs.Request;
using Financias.API.Domain.DTOs.Response;
using Financias.API.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Financias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly CategoriaService _categoriaService;
 
    public CategoriaController(CategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }
 
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaResponse>> ObterCategoria(int id)
    {
        var categoria = await _categoriaService.GetCategoriaByIdAsync(id);
        return Ok(categoria);
    }
    
    [HttpPost]
    public async Task<ActionResult<CategoriaResponse>> CriarCategoria([FromBody] CategoriaRequest request)
    {
        var categoriaResponse = await _categoriaService.CreateCategoriaAsync(request);
        return CreatedAtAction(nameof(ObterCategoria), new { id = categoriaResponse.Id }, categoriaResponse);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<CategoriaResponse>>> GetAll()
    {
        var categorias = await _categoriaService.GetAllCategoria();
        return Ok(categorias); 
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _categoriaService.DeleteCategoriaAsync(id); 
        return NoContent();
    }
    
    [HttpPatch("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] AtualizarCategoriaRequest request)
    {
        var categoriaAtualizada = await _categoriaService.UpdateCategoriaAsync(id, request);
        return Ok(categoriaAtualizada);
    }
    
    
    
}