using AutoMapper;
using Financias.API.Domain.DTOs.Request;
using Financias.API.Domain.DTOs.Response;
using Financias.API.Domain.Interfaces;
using Financias.API.Domain.Models;

namespace Financias.API.Application.Services;

public class CategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;
 
    public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
    {
        _categoriaRepository = categoriaRepository;
        _mapper = mapper;
    }
 
    public async Task<CategoriaResponse?> GetUserByIdAsync(int id)
    {
        var categoria = await _categoriaRepository.BuscarPorIdAsync(id);
 
        if (categoria == null)
            return null;
 
        return _mapper.Map<CategoriaResponse>(categoria);
    }
    
    public async Task<CategoriaResponse> CreateCategoriaAsync(CategoriaRequest categoriaRequest)
    {
        var categoria = _mapper.Map<Categoria>(categoriaRequest);
        var categoriaCriado = await _categoriaRepository.CriarCategoriaAsync(categoria);
        
        return _mapper.Map<CategoriaResponse>(categoriaCriado);
    }
 
}