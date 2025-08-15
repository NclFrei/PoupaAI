using Chatbot.API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.API.Infrastructure.Data;

public class ChatbotContext : DbContext
{
    public ChatbotContext(DbContextOptions<ChatbotContext> options) : base(options)
    {
    }

    public DbSet<Transacao> Transacoes { get; set; }
}