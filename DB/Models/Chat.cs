using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace myChat.DB.Models
{
  public class Chat
  {
    [Key]
    public Guid Id { get; set; }
    public List<User> Users { get; set; } = new List<User>();
    public List<Message> Messages { get; set; } = new List<Message>();
  }
}