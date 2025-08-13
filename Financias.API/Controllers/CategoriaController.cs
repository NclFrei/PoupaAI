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
        if (categoria == null)
            return NotFound();
 
        return Ok(categoria);
    }
    
    [HttpPost]
    public async Task<ActionResult<CategoriaResponse>> CriarCategoria([FromBody] CategoriaRequest request)
    {
        var categoriaResponse = await _categoriaService.CreateCategoriaAsync(request);
        return CreatedAtAction(null, categoriaResponse); 
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Categoria>>> GetAll()
    {
        var categorias = await _categoriaService.GetAllCategoria();
        if (categorias == null || categorias.Count == 0)
            return NotFound("Nenhuma categoria encontrada.");

        return Ok(categorias);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        bool deleted = await _categoriaService.DeleteCategoriaAsync(id);
        if (!deleted)
            return NotFound("Categoria não encontrada.");

        return NoContent();
    }
    
    [HttpPatch("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] AtualizarCategoriaRequest request)
    {
        try
        {
            var categoriaAtualizada = await _categoriaService.UpdateCategoriaAsync(id, request);
            return Ok(categoriaAtualizada);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    
    
}