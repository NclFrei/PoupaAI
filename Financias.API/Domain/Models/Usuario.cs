namespace Financias.API.Domain.Models;

public class Usuario
{
    public int Id { get; set; }
    public int IdExterno { get; set; } // ID do usuário no sistema externo
    public string Nome { get; set; }
    public string Email { get; set; }
    public DateTime DataCadastro { get; set; }
}
