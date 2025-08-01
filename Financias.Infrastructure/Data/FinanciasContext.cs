using Financias.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financias.Infrastructure.Data;

public class FinanciasContext : DbContext
{
    public FinanciasContext(DbContextOptions<FinanciasContext> options) : base(options)
    { }

    public DbSet<Transacoes> Transacoes { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
}


