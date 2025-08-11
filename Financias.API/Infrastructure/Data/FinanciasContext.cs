using Financias.API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Financias.API.Infrastructure.Data;

public class FinanciasContext : DbContext
{
    public FinanciasContext(DbContextOptions<FinanciasContext> options) : base(options)
    { }

    public DbSet<Transacao> Transacoes { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
}
