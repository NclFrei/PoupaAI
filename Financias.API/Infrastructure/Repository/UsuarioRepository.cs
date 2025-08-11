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

    public bool ExisteUsuarioExterno(int idExternoUsuario)
    {
        return _context.Usuarios.Any(usuario => usuario.IdExterno == idExternoUsuario);
    }

    public async Task<Usuario> CreateUsuario(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
        return usuario;
    }
}
