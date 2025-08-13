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
 
    public async Task<CategoriaResponse?> GetCategoriaByIdAsync(int id)
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
    
    public async Task<List<Categoria?>> GetAllCategoria()
    {
        List<Categoria> categorias = await _categoriaRepository.ListarCategoriasAsync();

        if (categorias == null)
            return null;

        return categorias;
    }
    public async Task<bool> DeleteCategoriaAsync(int id)
    {
        var categoria = await _categoriaRepository.BuscarPorIdAsync(id);
        if (categoria == null)
            return false;

        return await _categoriaRepository.DeleteCategoriaAsync(categoria); 
    }

    public async Task<Categoria> UpdateCategoriaAsync(int id, AtualizarCategoriaRequest request)
    {
        var categoria = await _categoriaRepository.BuscarPorIdAsync(id);
        if (categoria == null)
            throw new InvalidOperationException("Categoria não encontrada.");

        _mapper.Map(request, categoria);
        
        await _categoriaRepository.AtualizarCategoriaAsync(categoria);
        return categoria;
    }
    

    
    
 
}