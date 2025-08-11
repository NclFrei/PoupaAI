namespace Financias.API.Domain.Models;

public class Usuario
{
    public int Id { get; set; }
    public int IdExterno { get; set; } 
    public string Nome { get; set; }
    public string Email { get; set; }
    public DateTime DataCadastro { get; set; }
    public decimal Saldo { get; set; }
}
