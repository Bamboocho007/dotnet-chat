namespace myChat.DTO
{
  public class UserConnectionDto
  {
    public string UserName { get; set; } = string.Empty;
    public Guid ChatId { get; set; }
  }
}