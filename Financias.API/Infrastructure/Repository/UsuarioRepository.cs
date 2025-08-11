using Financias.API.Domain.Interfaces;
using Financias.API.Domain.Models;
using Financias.API.Infrastructure.Data;

namespace Financias.API.Infrastructure.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly FinanciasContext _context;

    public UsuarioRepository(FinanciasContext context)
    {
        _context = context;
    }

   public async Task<Usuario?> BuscarPorIdAsync(int id)
   {
        return await _context.Usuarios.FindAsync(id);
   }

    public async Task<Usuario> CreateUsuario(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
        return usuario;
    }

    public async Task<Usuario> AtualizarAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public bool ExisteUsuarioExterno(int idExternoUsuario)
    {
        return _context.Usuarios.Any(usuario => usuario.IdExterno == idExternoUsuario);
    }
}
