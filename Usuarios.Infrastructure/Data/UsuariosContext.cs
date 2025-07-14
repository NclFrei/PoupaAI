using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.Models;

namespace Usuarios.Infrastructure.Data;

public class UsuariosContext : DbContext
{
    public UsuariosContext(DbContextOptions<UsuariosContext> options) : base(options)
    { }
    public DbSet<Usuario> Usuarios { get; set; }
}
