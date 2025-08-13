using Financias.API.Domain.Models;

namespace Financias.API.Domain.Interfaces;

public interface ICategoriaRepository
{
    Task<Categoria?> BuscarPorIdAsync(int id);
    Task<Categoria> CriarCategoriaAsync(Categoria usuario);
    Task<List<Categoria>> ListarCategoriasAsync();
    Task<bool> DeleteCategoriaAsync(Categoria categoria);
    Task<Categoria> AtualizarCategoriaAsync(Categoria categoria);
}