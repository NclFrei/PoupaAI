using Financias.API.Application.Services;
using Financias.API.Domain.DTOs.Request;
using Financias.API.Domain.DTOs.Response;
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
        var categoria = await _categoriaService.GetUserByIdAsync(id);
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
}