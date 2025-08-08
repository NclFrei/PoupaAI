using Microsoft.EntityFrameworkCore;
using Usuarios.API.Domain.Models;

namespace Usuarios.API.Infrastructure.Data;

public class UsuariosContext : DbContext
{
    public UsuariosContext(DbContextOptions<UsuariosContext> options) : base(options)
    { }
    public DbSet<Usuario> Usuarios { get; set; }
}
