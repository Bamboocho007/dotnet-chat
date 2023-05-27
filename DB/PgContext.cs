
using Microsoft.EntityFrameworkCore;
using myChat.DB.Models;

namespace myChat.DB
{
  public class PgContext : DbContext
  {
    public DbSet<Chat> Chats { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    public PgContext(DbContextOptions<PgContext> options)
         : base(options)
    {
    }
  }
}