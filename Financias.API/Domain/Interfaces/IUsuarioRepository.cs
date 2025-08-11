using Financias.API.Domain.Models;

namespace Financias.API.Domain.Interfaces;

public interface IUsuarioRepository
{
    bool ExisteUsuarioExterno(int idExternoUsuario);

    public Task<Usuario> CreateUsuario(Usuario usuario);
}
