using Microsoft.EntityFrameworkCore;
using Usuarios.API.Models;

namespace Usuarios.API.Data;

public class UsuariosContext : DbContext
{
    public UsuariosContext(DbContextOptions<UsuariosContext> options) : base(options)
    { }
    public DbSet<Usuario> Usuarios { get; set; }
}
