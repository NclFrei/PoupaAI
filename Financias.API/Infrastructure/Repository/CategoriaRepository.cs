using Financias.API.Domain.Interfaces;
using Financias.API.Domain.Models;
using Financias.API.Infrastructure.Data;

namespace Financias.API.Infrastructure.Repository;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly FinanciasContext _context;
 
    public CategoriaRepository(FinanciasContext context)
    {
        _context = context;
    }
 
    public async Task<Categoria?> BuscarPorIdAsync(int id)
    {
        return await _context.Categorias.FindAsync(id);
    }
 
    public async Task<Categoria> CriarCategoriaAsync(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }
}