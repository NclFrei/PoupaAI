using Financias.API.Domain.Models;
using System.Threading.Tasks;

namespace Financias.API.Domain.Interfaces;

public interface IUsuarioRepository
{
    bool ExisteUsuarioExterno(int idExternoUsuario);
    Task<Usuario> CreateUsuario(Usuario usuario);
    Task<Usuario?> BuscarPorIdAsync(int id);
    Task<Usuario> AtualizarAsync(Usuario usuario);
}
