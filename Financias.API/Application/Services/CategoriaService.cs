using AutoMapper;
using Financias.API.Domain.DTOs.Request;
using Financias.API.Domain.DTOs.Response;
using Financias.API.Domain.Interfaces;
using Financias.API.Domain.Models;
using FluentValidation;

namespace Financias.API.Application.Services;

public class CategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CategoriaRequest> _validator;
 
    public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper, IValidator<CategoriaRequest> validator)
    {
        _categoriaRepository = categoriaRepository;
        _mapper = mapper;
        _validator = validator;
    }
 
    public async Task<CategoriaResponse?> GetCategoriaByIdAsync(int id)
    {
        var categoria = await _categoriaRepository.BuscarPorIdAsync(id);
        if (categoria == null)
            throw new KeyNotFoundException("Categoria não encontrada.");

        return _mapper.Map<CategoriaResponse>(categoria);
    }
    
    public async Task<CategoriaResponse> CreateCategoriaAsync(CategoriaRequest categoriaRequest)
    {
        var result = await _validator.ValidateAsync(categoriaRequest);

        if (!result.IsValid)
        {
            var errors = result.Errors
                .Select(e => $"{e.PropertyName}: {e.ErrorMessage} ")
                .ToList();

            throw new ValidationException(string.Join(Environment.NewLine, errors));
        }
        
        var categoria = _mapper.Map<Categoria>(categoriaRequest);
        var categoriaCriado = await _categoriaRepository.CriarCategoriaAsync(categoria);
        
        return _mapper.Map<CategoriaResponse>(categoriaCriado);
    }
    
    public async Task<List<CategoriaResponse>> GetAllCategoria()
    {
        var categorias = await _categoriaRepository.ListarCategoriasAsync();
        return _mapper.Map<List<CategoriaResponse>>(categorias ?? new List<Categoria>());
    }
    
    public async Task DeleteCategoriaAsync(int id)
    {
        var categoria = await _categoriaRepository.BuscarPorIdAsync(id);
        if (categoria == null)
            throw new KeyNotFoundException("Categoria não encontrada.");

        await _categoriaRepository.DeleteCategoriaAsync(categoria);
    }

    public async Task<Categoria> UpdateCategoriaAsync(int id, AtualizarCategoriaRequest request)
    {
        var categoria = await _categoriaRepository.BuscarPorIdAsync(id);
        if (categoria == null)
            throw new KeyNotFoundException("Categoria não encontrada.");

        _mapper.Map(request, categoria);
        
        await _categoriaRepository.AtualizarCategoriaAsync(categoria);
        return categoria;
    }
    

    
    
 
}