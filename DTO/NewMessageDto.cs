namespace myChat.DTO
{
  public class NewMessageDto
  {
    public string Text { get; set; } = string.Empty;
    public Guid UserId { get; set; }
  }
}