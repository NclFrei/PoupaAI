using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuarios.Domain.Models;

public class Usuarios
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; } = DateTime.Now;
    public DateTime DataNascimento { get; set; }

    public void SetPassword(string senha)
    {
        Senha = BCrypt.Net.BCrypt.HashPassword(senha);
    }

    public bool CheckPassword(string senha)
    {
        return BCrypt.Net.BCrypt.Verify(senha, Senha);
    }
}
