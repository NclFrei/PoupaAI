using Usuarios.API.Models;

namespace Usuarios.API.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> BuscarPorIdAsync(int id);
    Task<Usuario?> BuscarPorEmailAsync(string email);
    Task<bool> VerificaEmailExisteAsync(string email);
    Task<Usuario> CriarAsync(Usuario usuario);
    Task<bool> DeleteAsync(Usuario usuario);
    Task<Usuario> AtualizarAsync(Usuario usuario);
}
