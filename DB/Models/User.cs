using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace myChat.DB.Models
{
  public class User
  {
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [JsonIgnore]
    public List<Chat> Chats { get; private set; } = new List<Chat>();
    public List<Message> Messages { get; private set; } = new List<Message>();
  }
}