using System.ComponentModel.DataAnnotations;

namespace myChat.DB.Models
{
  public class Message
  {
    [Key]
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public User? User { get; private set; } = new();
    public Chat? Chat { get; private set; } = new();
  }
}