namespace Chatbot.API.Domain.Models;

public class Transacao
{
    public int Id { get; set; }
    public int IdExterno { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public DateTime DataTransacao { get; set; }
    public decimal Valor { get; set; }
    public string Categoria { get; set; }
    public string Tipo { get; set; }
    public string UsuarioNome { get; set; }
    public int UsuarioId { get; set; }
}